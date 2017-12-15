using GamePool.BLL.LogicContracts;
using System.Web.Mvc;
using System.Net;
using System.Configuration;
using AutoMapper;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Shared;
using System;
using GamePool.PL.MVC.Models.Product;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace GamePool.PL.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly int PageSize;
        private readonly int MaxPageSelectors;
        private readonly IGameLogic gameLogic;
        private readonly IGenreLogic genreLogic;
        private readonly IOrderLogic orderLogic;

        private IEnumerable<OrderedGameVM> OrderList
        {
            get => Session["OrderedGames"] as IEnumerable<OrderedGameVM>;
        }


        public ProductController(IGameLogic gameLogic, IGenreLogic genreLogic, IOrderLogic orderLogic)
        {
            this.PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            this.MaxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);
            this.gameLogic = gameLogic;
            this.genreLogic = genreLogic;
            this.orderLogic = orderLogic;
        }

        [HttpGet]
        public ActionResult Index(int? pageNumber = 1)
        {
            var games = this.gameLogic.GetAll(pageNumber.Value, this.PageSize);
            var gamePreviews = Mapper.Map<IEnumerable<GameEntity>, IEnumerable<GamePreviewVM>>(games.Data);

            return View(new PagedItems<GamePreviewVM>
            {
                Data = gamePreviews,
                CurrentPage = pageNumber.Value,
                MaxPageSelectors = this.MaxPageSelectors,
                TotalPages = (int)Math.Ceiling((double)games.Count / this.PageSize)
            });
        }

        [HttpGet]
        [Authorize]
        public ActionResult Cart()
        {
            var orderedGames = Session["OrderedGames"] as IEnumerable<OrderedGameVM>;

            if (orderedGames == null)
            {
                return View(Enumerable.Empty<OrderedGameVM>());
            }

            return View(orderedGames);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var gameEntity = this.gameLogic.GetById(id.Value);

            if (gameEntity == null)
            {
                return HttpNotFound();
            }

            var game = Mapper.Map<GameEntity, DisplayGameVM>(gameEntity);
            var genres = this.genreLogic.GetByGameId(game.Id);

            game.Genres = genres != null ? string.Join(", ", genres.Select(g => g.Name)) : null;

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
        public ActionResult Checkout(OrderVM orderVM)
        {
            if (orderVM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                if (OrderList != null)
                {
                    foreach (var order in OrderList)
                    {
                        this.orderLogic.Add(new Order
                        {
                            Name = orderVM.Name,
                            Surname = orderVM.Surname,
                            Email = orderVM.Email,
                            PhoneNumber = orderVM.PhoneNumber,
                            Quantity = order.Quantity,
                            GameId = order.Id
                        });
                    }

                    SendEmail(OrderList, orderVM);


                    Session["OrderedGames"] = null;                    
                }
                
                return RedirectToAction("Index");
            }

            return View(orderVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int id)
        {
            if (this.gameLogic.Remove(id))
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Details", new { id = id });
        }

        private void SendEmail(IEnumerable<OrderedGameVM> orderedGames, OrderVM checkoutVM)
        {
            const string fromEmail = "qwertysakkal@gmail.com";
            const string fromPassword = "ruslan1998";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
            };

            StringBuilder msgBody = new StringBuilder();

            msgBody.AppendFormat("Hello, {0} {1}.\nThanks for buying this games:\n", checkoutVM.Name, checkoutVM.Surname);

            int gameCount = 1;
            foreach (var game in orderedGames)
            {
                msgBody.AppendFormat("\n\t{0}) Name: {1}, Price: {2}$, Quantity: {3}\n", gameCount++, game.Name, Math.Round(game.Price), game.Quantity);
            }

            msgBody.AppendFormat(
                "\nTotal price: {0}$",
                Math.Round(orderedGames.Select(g => g.Quantity * g.Price).Aggregate((a, b) => a + b)));

            MailMessage mail = new MailMessage(fromEmail, checkoutVM.Email)
            {
                Subject = "Thanks for buying a games",
                Body = msgBody.ToString()
            };

            smtpClient.Send(mail);
        }
    }
}