using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamePool.PL.MVC.Models.Search;

namespace GamePool.PL.MVC.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index(SearchParametersVM parameters)
        {
            return View(parameters);
        }
    }
}