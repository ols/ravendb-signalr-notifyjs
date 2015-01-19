using System.Web.Mvc;
using rampsnamp.Core;

namespace rampsnamp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserQuery _userQuery;
        private readonly IUserService _userService;

        public HomeController(
            IUserQuery userQuery, 
            IUserService userService)
        {
            _userQuery = userQuery;
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CreateUser(CreateUserCommand model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {Success = false, Message = "Please fill in everything..."});
            }

            var user = _userQuery.GetUserByEmail(model.Email);

            if (user == null)
            {
                _userService.CreateUser(new CreateUserCommand
                {
                    Email = model.Email,
                    Firstname = model.Firstname
                });
            }
            else
            {
               return Json(new { Success = false, Message = "User already exists" });
            }

            return Json(new { Success = true });
        }
    }
}