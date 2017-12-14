using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Product;

namespace GamePool.PL.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IGameLogic gameLogic;
        private readonly IOrderLogic orderLogic;

        private readonly string CartKey = "OrderedGames";

        private List<OrderedGameVM> OrderList {
            get
            {
                if (Session[this.CartKey] == null)
                {
                    var orderedList = new List<OrderedGameVM>();

                    Session[this.CartKey] = orderedList;

                    return orderedList;
                }
                else
                {
                    return Session[this.CartKey] as List<OrderedGameVM>;
                }
            }}

        public OrderController(IGameLogic gameLogic, IOrderLogic orderLogic)
        {
            this.gameLogic = gameLogic;
            this.orderLogic = orderLogic;
        }

        [HttpGet]
        public ActionResult AddGameToOrders(int gameId)
        {
            var game = this.gameLogic.GetById(gameId);
            var orderedGame = Mapper.Map<GameEntity, OrderedGameVM>(game);
            var result = false;

            orderedGame.Quantity = 1;

            if (OrderList?.FirstOrDefault(g => g.Id == gameId) == null)
            {
                OrderList?.Add(orderedGame);
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RemoveGameFromOrders(int gameId)
        {
            var result = false;

            result = OrderList?.RemoveAll(x => x.Id == gameId) > 0;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateGameQuantity(int gameId, int quantity)
        {
            var result = false;

            var gameForUpdate = OrderList?.FirstOrDefault(g => g.Id == gameId);

            if (gameForUpdate != null)
            {
                result = true;
                gameForUpdate.Quantity = quantity;
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }
    }
}