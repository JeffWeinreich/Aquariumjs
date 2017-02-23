using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular.Web.Controllers     //Aquarium.Controllers
{
    public class HomeController : Controller
    {
       // GET: /<controller>/
        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return Redirect("~/home/user");
            //}
            //else
            //{
            //    return Redirect("~/accounts/login");
            //}

            return View();

        }

        public IActionResult FishAdd()
        {
            return View();
        }

        public IActionResult User()
        {
            return View();
        }


    }
}
