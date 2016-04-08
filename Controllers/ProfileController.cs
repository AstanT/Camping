using System.Web.Mvc;
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
            return View();
        }



    }
}