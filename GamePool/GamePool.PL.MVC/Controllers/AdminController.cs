using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamePool.PL.MVC.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public ActionResult UserList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Orders()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddGame()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditGame()
        {
            return View();
        }
    }
}