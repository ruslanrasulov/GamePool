using GamePool.BLL.LogicContracts;
using System.Web.Mvc;

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
    }
}