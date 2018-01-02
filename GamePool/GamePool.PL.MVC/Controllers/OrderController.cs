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
        private readonly IGameLogic _gameLogic;

        private readonly string CartKey = "OrderedGames";

        private List<OrderedGameVm> OrderList {
            get
            {
                if (!(Session[CartKey] is List<OrderedGameVm>) || Session[CartKey] == null)
                {
                    var orderedList = new List<OrderedGameVm>();

                    Session[CartKey] = orderedList;

                    return orderedList;
                }
                else
                {
                    return Session[CartKey] as List<OrderedGameVm>;
                }
            }}

        public OrderController(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddGameToOrders(int gameId)
        {
            var game = _gameLogic.GetById(gameId);
            var orderedGame = Mapper.Map<GameEntity, OrderedGameVm>(game);
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
        [Authorize]
        public ActionResult RemoveGameFromOrders(int gameId)
        {
            var result = OrderList?.RemoveAll(x => x.Id == gameId) > 0;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
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
    }
}