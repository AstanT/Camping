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
    public class ProfileController : Controller
    {
        private readonly IUserManager<User> _userManager;
        private readonly IOrderManager<Order> _orderManager; 

        public ProfileController(IUserManager<User> userManager, IOrderManager<Order> orderManager)
        {
            _userManager = userManager;
            _orderManager = orderManager;
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

        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel model, HttpPostedFileBase upload)
        {
            try
            {
                var user = _userManager.GetById(model.Id);
                if (upload != null && upload.ContentLength > 0)
                {
                    //var pic = new AddPhotos();
                    //pathPic = pic.AddImage(upload, Server.MapPath("~/Images/Account/"), "~/Images/Account/");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }*/

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
    }
}