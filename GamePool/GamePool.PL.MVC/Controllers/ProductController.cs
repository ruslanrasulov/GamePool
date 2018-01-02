using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Shared;
using GamePool.PL.MVC.Models.Product;

namespace GamePool.PL.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGameLogic _gameLogic;
        private readonly IGenreLogic _genreLogic;
        private readonly IOrderLogic _orderLogic;

        private readonly int _pageSize;
        private readonly int _maxPageSelectors;
        private readonly string _cartKey = "OrderedGames";
        
        private IEnumerable<OrderedGameVm> OrderList 
            => Session[_cartKey] as IEnumerable<OrderedGameVm>;
        
        public ProductController(IGameLogic gameLogic, IGenreLogic genreLogic, IOrderLogic orderLogic)
        {
            _pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            _maxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);

            _gameLogic = gameLogic;
            _genreLogic = genreLogic;
            _orderLogic = orderLogic;
        }

        [HttpGet]
        public ActionResult Index(int? pageNumber = 1)
        {
            var games = _gameLogic.GetAll(pageNumber.Value, _pageSize);
            var gamePreviews = Mapper.Map<IEnumerable<GameEntity>, IEnumerable<GamePreviewVm>>(games.Data);

            var totalPages = (int)Math.Ceiling((double)games.Count / _pageSize);

            return View(new PagedItems<GamePreviewVm>
            {
                Data = gamePreviews,
                PageInfo = new PageInfo
                {
                    CurrentPage = pageNumber.Value,
                    StartIndex = GetStartIndex(pageNumber.Value, totalPages),
                    PageLinksCount = (totalPages < _maxPageSelectors) ? totalPages : _maxPageSelectors,
                    TotalPages = totalPages
                }
            });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Cart()
        {
            if (OrderList == null)
            {
                return View(Enumerable.Empty<OrderedGameVm>());
            }

            return View(OrderList);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gameEntity = _gameLogic.GetById(id.Value);

            if (gameEntity == null)
            {
                return HttpNotFound();
            }

            var game = Mapper.Map<GameEntity, DisplayGameVm>(gameEntity);
            var genres = _genreLogic.GetByGameId(game.Id);

            game.Genres = genres != null ?
                string.Join(", ", genres.Select(g => g.Name)) 
                : null;

            return View(game);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(OrderVm orderVm)
        {
            if (orderVm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                if (OrderList != null)
                {
                    foreach (var order in OrderList)
                    {
                        _orderLogic.Add(new Order
                        {
                            Name = orderVm.Name,
                            Surname = orderVm.Surname,
                            Email = orderVm.Email,
                            PhoneNumber = orderVm.PhoneNumber,
                            Quantity = order.Quantity,
                            GameId = order.Id
                        });
                    }

                    SendEmail(OrderList, orderVm);


                    Session["OrderedGames"] = null;                    
                }
                
                return RedirectToAction("Index");
            }

            return View(orderVm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int id)
        {
            return _gameLogic.Remove(id) ? RedirectToAction("Index") : RedirectToAction("Details", new { id });
        }

        private void SendEmail(IEnumerable<OrderedGameVm> orderedGames, OrderVm checkoutVm)
        {
            var fromEmail = ConfigurationManager.AppSettings["EmailLogin"];
            var fromPassword = ConfigurationManager.AppSettings["EmailPassword"];

            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
            };        

            var mail = new MailMessage(fromEmail, checkoutVm.Email)
            {
                Subject = "Thanks for buying a games",
                Body = BuildBody(orderedGames, checkoutVm)
            };

            smtpClient.Send(mail);
        }

        private string BuildBody(IEnumerable<OrderedGameVm> orderedGames, OrderVm checkoutVm)
        {
            var msgBody = new StringBuilder();
            msgBody.AppendFormat("Hello, {0} {1}.\nThanks for buying this games:\n", checkoutVm.Name, checkoutVm.Surname);

            var gameCount = 1;

            foreach (var game in orderedGames)
            {
                msgBody.AppendFormat("\n\t{0}) Name: {1}, Price: {2}$, Quantity: {3}\n", gameCount++, game.Name, Math.Round(game.Price), game.Quantity);
            }

            msgBody.AppendFormat(
                "\nTotal price: {0}$",
                Math.Round(orderedGames.Select(g => g.Quantity * g.Price).Aggregate((a, b) => a + b)));

            return msgBody.ToString();
        }

        private int GetStartIndex(int currentPage, int totalPages)
        {
            if (totalPages <= _maxPageSelectors)
            {
                return 1;
            }

            var rightDifference = totalPages - currentPage;
            var middle = _maxPageSelectors / 2;

            if (currentPage < middle)
            {
                return 1;
            }

            if (rightDifference < middle)
            {
                return totalPages - _maxPageSelectors + 1;
            }

            return currentPage - middle;
        }
    }
}