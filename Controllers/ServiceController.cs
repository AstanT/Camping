using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Camping.BL;
using Camping.Core;
using Camping.Filters;
using Camping.Interfaces.Manager;
using Camping.ViewModels;

namespace Camping.Controllers
{
    [Culture]
    public class ServiceController : Controller
    {
        private readonly IServicesManager<Services> _servicesManager;
        private readonly IServicePhotoManager<ServicePhoto> _servicePhotoManager;
        private readonly ICommentManager<Comments> _commentManager;
        private readonly IOrderManager<Order> _orderManager;

        public ServiceController(IServicesManager<Services> servicesManager,
            IServicePhotoManager<ServicePhoto> servicePhotoManager, ICommentManager<Comments> commentManager,
            IOrderManager<Order> orderManager)
        {
            _servicesManager = servicesManager;
            _servicePhotoManager = servicePhotoManager;
            _commentManager = commentManager;
            _orderManager = orderManager;
        }

        [AllowAnonymous]
        public ActionResult ServicePage(ServicePageViewModel model, long id)
        {
            try
            {
                var service = _servicesManager.GetById(id);
                model = Mapper.Map<Services, ServicePageViewModel>(service);
                model.IdUserInSystem = Convert.ToInt64(Session["UserId"]);
                List<PhotoViewModel> photos = new List<PhotoViewModel>();
                IQueryable<ServicePhoto> photosList;
                photosList =
                    _servicePhotoManager.GetServicePhotosByServiceId(service.id).OrderByDescending(x => x.photo.date);
                foreach (var photo in photosList)
                {
                    photos.Add(Mapper.Map<ServicePhoto, PhotoViewModel>(photo));
                }
                model.Photos = photos;

                var commentsService =
                    _commentManager.GetCommentsByServiceId(service.id).OrderByDescending(x => x.dateOfComment);
                List<CommentViewModel> comments = new List<CommentViewModel>();
                foreach (var comment in commentsService)
                {
                    comments.Add(Mapper.Map<Comments,CommentViewModel>(comment));
                }
                model.Comments = comments;

                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }            
        }

        [AllowAnonymous]
        public ActionResult DeleteComment(long id)
        {
            var comment = _commentManager.GetById(id);
            _commentManager.Delete(comment);
            return RedirectToRoute("ServicePage", new {id = comment.id_service});
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddComment(ServicePageViewModel model)
        {
            if (model.NewComment == null)
            {
                return RedirectToRoute("ServicePage", new { id =  model.Id });
            }
            var entity = Mapper.Map<ServicePageViewModel, Comments>(model);
            _commentManager.Add(entity);
            return RedirectToRoute("ServicePage", new { id = model.Id });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddPhotoService(ServicePageViewModel model, HttpPostedFileBase upload)
        {
            try
            {
                var pic = new AddPhotos();
                var pathPic = pic.AddImage(upload, Server.MapPath("/images/Services/"), "/images/Services/");
                var entity = new ServicePhoto()
                {
                    id_service = model.Id,
                    photo = new Photo()
                    {
                        name = pathPic,
                        date = DateTime.Now,
                    }
                };
                _servicePhotoManager.Add(entity);
                return RedirectToRoute("ServicePage", new { id = model.Id });
            }
            catch (Exception)
            {
                return RedirectToRoute("ServicePage", new { id = model.Id });
            }
        }

        
        [AllowAnonymous]
        public ActionResult ListServises(ListServisesViewModel model, string type)
        {
            List<ServicesViewModel> services = new List<ServicesViewModel>();
            List<PhotoViewModel> photos = new List<PhotoViewModel>();
            IQueryable<Services> listServices;

            if (model.Search != null)
            {
                if (model.Search.PriceEnd == 0)
                {
                    model.Search.PriceEnd = long.MaxValue;
                }
                listServices =
                    _servicesManager.Serch(type, model.Search.Name, Convert.ToInt16(model.Search.Rating), model.Search.PriceStart,
                        model.Search.PriceEnd).OrderByDescending(x => x.rating);
            }
            else
            {
                listServices = _servicesManager.GetServices(type).OrderByDescending(x => x.rating);
            }           

            foreach (var house in listServices)
            {
                var photosHouse = _servicePhotoManager.GetServicePhotosByServiceId(house.id);
                photos.Clear();
                foreach (var photo in photosHouse)
                {
                    photos.Add(Mapper.Map<ServicePhoto, PhotoViewModel>(photo));
                }
                var home = Mapper.Map<Services, ServicesViewModel>(house);
                home.Photo = photos[0].Name;
                services.Add(home);
            }
            model.Type = type;
            model.SeervicesList = services;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddService(AddServiceViewModel model, HttpPostedFileBase upload)
        {
            try
            {
                var pic = new AddPhotos();
                var pathPic = pic.AddImage(upload, Server.MapPath("/images/Services/"), "/images/Services/");
                var entity = Mapper.Map<AddServiceViewModel, Services>(model);
                entity.servicePhoto = new List<ServicePhoto>()
                {
                    new ServicePhoto
                    {
                        photo = new Photo
                        {
                            name = pathPic,
                            date = DateTime.Now
                        }
                    }
                };
                _servicesManager.Add(entity);

                return RedirectToRoute("UserPage");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult NewRating(ServicePageViewModel model)
        {
            try
            {
                var service = _servicesManager.GetById(model.Id);
                service.rating = (short) ((service.rating + model.NewRating)/2);
                _servicesManager.Update(service);
                return RedirectToRoute("ServicePage", new { id = model.Id });
            }
            catch (Exception)
            {
                return RedirectToRoute("ServicePage", new { id = model.Id });
            }
        }

        [AllowAnonymous]
        public ActionResult DeleteService(long id)
        {
            var listComments = new List<Comments>();
            var listOrders = new List<Order>();
            var listPhotos = new List<ServicePhoto>();

            var comments = _commentManager.GetCommentsByServiceId(id);
            var orders = _orderManager.GetOrdersByServiceId(id);
            var photos = _servicePhotoManager.GetServicePhotosByServiceId(id);

            foreach (var comment in comments)
            {
                listComments.Add(comment);
            }
            foreach (var order in orders)
            {
                listOrders.Add(order);
            }
            foreach (var photo in photos)
            {
                listPhotos.Add(photo);
            }

            foreach (var coment in listComments)
            {
                _commentManager.Delete(coment);
            }
            foreach (var order in listOrders)
            {
                _orderManager.Delete(order);
            }
            foreach (var photo in listPhotos)
            {
                _servicePhotoManager.Delete(photo);
            }

            var service = _servicesManager.GetById(id);
            _servicesManager.Delete(service);
            return RedirectToRoute("UserPage", new { id = 1 });
        }

    }
}