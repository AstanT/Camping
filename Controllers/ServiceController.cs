using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
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

        public ServiceController (IServicesManager<Services> servicesManager, IServicePhotoManager<ServicePhoto> servicePhotoManager  )
        {
            _servicesManager = servicesManager;
            _servicePhotoManager = servicePhotoManager;
        }

        [AllowAnonymous]
        public ActionResult ServicePage(ServicePageViewModel model, long id)
        {
            try
            {
                var service = _servicesManager.GetById(id);
                model = Mapper.Map<Services, ServicePageViewModel>(service);

                List<PhotoViewModel> photos = new List<PhotoViewModel>();
                IQueryable<ServicePhoto> photosList;
                photosList =
                    _servicePhotoManager.GetServicePhotosByServiceId(service.id).OrderByDescending(x => x.photo.date);
                foreach (var photo in photosList)
                {
                    photos.Add(Mapper.Map<ServicePhoto, PhotoViewModel>(photo));
                }
                model.Photos = photos;

                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }            
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult House(HouseViewModel model)
        {
            List<ServicesViewModel> services = new List<ServicesViewModel>();
            List<PhotoViewModel> photos = new List<PhotoViewModel>();
            IQueryable<Services> houses;

            houses = _servicesManager.GetHouse("House").OrderByDescending(x => x.rating);

            foreach (var house in houses)
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
            model.SeervicesList = services;
            return View(model);
        }
    }
}