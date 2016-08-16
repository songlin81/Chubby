using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using KendoMVC.Models.Entities;

namespace KendoMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            //return UserRepository.FindAllRoles();

            return View();
        }

        public JsonResult GetRoles()
        {
            var roles = new List<Role> { 
                new Role { Number = 123, RoleName = "admin" }, 
                new Role { Number = 234, RoleName = "opt" } 
            };
            //var domainRoles = userService.GetRoles();
            //var roles = Mapper.Map<IList<UserDomainEntities.Role>, IList<Role>>(domainRoles);
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
