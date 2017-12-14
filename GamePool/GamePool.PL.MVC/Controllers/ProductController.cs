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

namespace GamePool.PL.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly int PageSize;
        private readonly int MaxPageSelectors;
        private readonly IGameLogic gameLogic;
        private readonly IGenreLogic genreLogic;

        public ProductController(IGameLogic gameLogic, IGenreLogic genreLogic)
        {
            this.PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            this.MaxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);
            this.gameLogic = gameLogic;
            this.genreLogic = genreLogic;
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
    }
}