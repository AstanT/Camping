using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
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
    public class AccountController : Controller
    {
        private readonly IUserManager<User> _userManager;

        public AccountController(IUserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterViewModel model)
        {
            try
            {
                var entity = Mapper.Map<RegisterViewModel, User>(model);
                var user = _userManager.GetUserByEmail(model.Email);
                if (user != null) throw new Exception("лваолв");

                var salt = PasswordHashing.GenerateSaltValue();
                var pass = PasswordHashing.HashPassword(entity.password, salt);
                entity.passwordSalt = salt;
                entity.password = pass;
                entity.photo = "/images/Account/account.jpg";
                _userManager.Add(entity);

                entity.roles = new List<UserInRoles>() { new UserInRoles() { id_roles = 2, id_user = entity.id } };
                _userManager.Update(entity);

                var url = Url.Action("ConfirmEmail", "Account", new { token = entity.id, email = entity.email },
                    Request.Url.Scheme);
                _userManager.SentConfirmMail(entity, url);


                return RedirectToRoute("AfterRegistration");
            }
            catch (Exception e)
            {
                model.Error = e.Message;
                return View(model);
            }
            
        }

        [AllowAnonymous]
        public ActionResult EndRegistration()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConfirmEmail(long token, string email)
        {
            try
            {
                var user = _userManager.GetById(token);
                _userManager.ActivateUser(user);
                return View();
            }
            catch (Exception)
            {
                return RedirectToRoute("Default");
            }
        }


        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel model)
        {
            try
            {
                var user = _userManager.GetUserByEmail(model.EmailLogin);
                if(user == null) throw new Exception(Resource.EmailNotRegistered);
                var passLogin = PasswordHashing.HashPassword(model.PasswordLogin, user.passwordSalt);
                if(user.password != passLogin) throw  new Exception(Resource.WrongPassword);
                if(!user.isActivated) throw new Exception(Resource.NotActived);
                FormsAuthentication.SetAuthCookie(user.email, false);
                return RedirectToAction("UserPage", "Profile", user.id);
            }
            catch (Exception e)
            {
                model.Error = e.Message;
                return View(model);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }




    }
}
