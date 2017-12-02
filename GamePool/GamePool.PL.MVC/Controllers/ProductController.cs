using GamePool.BLL.LogicContracts;
using System.Web.Mvc;
using System.Net;

namespace GamePool.PL.MVC.Controllers
{
    public class ProductController : Controller
    {
        private IGameLogic gameLogic;

        public ProductController(IGameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Cart()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}