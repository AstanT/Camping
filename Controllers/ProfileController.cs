using System;
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

        public ProfileController(IUserManager<User> userManager)
        {
            _userManager = userManager;
        }
        
        public ActionResult UserPage(long? id)
        {
            if (id == null) return UserPage(new UserPageViewModel(), id);
            var user = _userManager.GetUserByEmail(User.Identity.Name);
            Session["UserId"] = user.id;
            return UserPage(new UserPageViewModel(), id);
        }

        [HttpPost]
        public ActionResult UserPage(UserPageViewModel model, long? id)
        {
            var user = _userManager.GetById((long)id);
            model = Mapper.Map<User,UserPageViewModel>(user);

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
                if (upload != null && upload.ContentLength > 0)
                {
                    var pic = new AddPhotos();
                    pathPic = pic.AddImage(upload, Server.MapPath("~/Images/Account/"), "~/Images/Account/");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }


    }
}