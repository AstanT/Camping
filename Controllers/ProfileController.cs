using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Camping.App_GlobalResources;
using Camping.BL;
using Camping.Core;
using Camping.Filters;
using Camping.Interfaces.Manager;
using Camping.ViewModels;

namespace Camping.Controllers
{
    [Culture]
    public class ProfileController : Controller
    {
        private readonly IUserManager<User> _userManager;
        private readonly IOrderManager<Order> _orderManager;
        private readonly IServicesManager<Services> _servicesManager;

        public ProfileController(IUserManager<User> userManager, IOrderManager<Order> orderManager,
            IServicesManager<Services> servicesManager)
        {
            _userManager = userManager;
            _orderManager = orderManager;
            _servicesManager = servicesManager;
        }

        public ActionResult UserPage(long? id)
        {
            if (id != null) return UserPage(new UserPageViewModel(), id);
            var user = _userManager.GetUserByEmail(User.Identity.Name);
            Session["UserId"] = user.id;
            return UserPage(new UserPageViewModel(), user.id);
        }

        [HttpPost]
        public ActionResult UserPage(UserPageViewModel model, long? id)
        {
            if (!ModelState.IsValid) return View();
            var user = _userManager.GetById((long)id);
            model = Mapper.Map<User,UserPageViewModel>(user);

            int coutOrder = 0;
            List<OrderViewModel> orders = new List<OrderViewModel>();
            IQueryable<Order> ordersList;

            ordersList = _orderManager.GetOrdersByUserId(user.id).OrderByDescending(x => x.dateOrder);

            foreach (var order in ordersList)
            {               
                orders.Add(Mapper.Map<Order, OrderViewModel>(order));
                if (coutOrder == 2) break;
                coutOrder++;

            }
            model.LastUserOrders = orders;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EditProfile(EditProfileViewModel model, long id)
        {
            var user = _userManager.GetById(id);
            model = Mapper.Map<User, EditProfileViewModel>(user);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel model, HttpPostedFileBase upload)
        {
            try
            {
                var user = _userManager.GetById(model.Id);

                string pathPic = user.photo;

                if (upload != null && upload.ContentLength > 0)
                {
                    var pic = new AddPhotos();
                    pathPic = pic.AddImage(upload, Server.MapPath("/images/Account/"), "/images/Account/");
                }

                user = Mapper.Map<EditProfileViewModel, User>(model, user);
                user.photo = pathPic;
                _userManager.Update(user);

                return RedirectToRoute("UserPage");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(EditPasswordViewModel model,long id)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                var user = _userManager.GetById(id);
                if (user.password != PasswordHashing.HashPassword
                    (model.Password, user.passwordSalt))
                    throw new Exception(Resource.WrongPassword);

                var newSalt = PasswordHashing.GenerateSaltValue();
                user.passwordSalt = newSalt;
                user.password = PasswordHashing.HashPassword(model.NewPassword, newSalt);
                _userManager.Update(user);
                return RedirectToRoute("UserPage");
            }
            catch (Exception e)
            {
                model.Error = e.Message;
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult UserOrders(UserOrdersPageViewModel model, long id)
        {
            try
            {
                List<OrderViewModel> orders = new List<OrderViewModel>();
                IQueryable<Order> ordersList;

                ordersList = _orderManager.GetOrdersByUserId(id).OrderByDescending(x => x.dateOrder);

                foreach (var order in ordersList)
                {
                    orders.Add(Mapper.Map<Order,OrderViewModel>(order));
                }
                model.UserOrders = orders;
                return View(model);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult NewOrder(ServicePageViewModel model)
        {
            try
            {
                var service = _servicesManager.GetById(model.Id);
                model.Price = service.price;
                var entity = Mapper.Map<ServicePageViewModel, Order>(model);
                _orderManager.Add(entity);
                return RedirectToRoute("UserPage");
            }
            catch (Exception)
            {
                return RedirectToRoute("ServicePage", new { id = model.Id });
            }            
        }

        [AllowAnonymous]
        public ActionResult DeleteOrder(long id)
        {
            var order = _orderManager.GetById(id);
            _orderManager.Delete(order);
            return RedirectToRoute("UserOrders");
        }

    }
}