using System;
using System.Collections.Generic;
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
                //var mapper = _mapper.Map()

                var entity = Mapper.Map<RegisterViewModel, User>(model);
                var user = _userManager.GetUserByEmail(model.Email);
                if (user != null) throw new Exception("лваолв");

                var salt = PasswordHashing.GenerateSaltValue();
                var pass = PasswordHashing.HashPassword(entity.password, salt);
                entity.passwordSalt = salt;
                entity.password = pass;
                _userManager.Add(entity);
                
                entity.roles = new List<UserInRoles>() { new UserInRoles() { id_roles = 2, id_user = entity.id } };
                _userManager.Update(entity);

                var url = Url.Action("ConfirmEmail", "Account", new { token = entity.id, email = entity.email },
                    Request.Url.Scheme);
                _userManager.SentConfirmMail(entity, url);


                return RedirectToRoute("AfterRegistration");

            
          



        }
    }
    }
