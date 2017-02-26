using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aquarium.Data;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular.Web.Controllers     //Aquarium.Controllers
{
    public class HomeController : Controller
    {
        private AquariumContext Context { get; set; }

        public HomeController()
        {
            Context = new AquariumContext();
        }
       // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Route("~/home/user/tanks/tank/fishadd")]
        public IActionResult FishAdd()
        {
            return View();
        }

        public IActionResult User()
        {
            return View();
        }

        [Route("~/home/user/tanks/tank/fish{id}")]
        public IActionResult Fish(int id)
        {
            var fish = Context.Fishes.Find(id);

            return View(fish);
        }

        [Route("~/home/user/tankadd")]
        public IActionResult TankAdd()
        {
            return View();
        }

        [Route("~/home/user/tanks")]
        public IActionResult Tanks()
        {
            return View();
        }

        [Route("~/home/user/tanks/{id}")]
        public IActionResult Tank(int id)
        {
            var tank = Context.Tanks.Find(id);

            return View(tank);
        }


    }
}
