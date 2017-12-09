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

namespace GamePool.PL.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly int PageSize;
        private readonly int MaxPageSelectors;
        private IGameLogic gameLogic;

        public ProductController(IGameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
            this.PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            this.MaxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);
        }

        [HttpGet]
        public ActionResult Index(int? page = 1)
        {
            var games = this.gameLogic.GetAll(page.Value, this.PageSize);
            var gamePreviews = Mapper.Map<IEnumerable<GameEntity>, IEnumerable<GamePreviewVM>>(games.Data);

            return View(new PagedItems<GamePreviewVM>
            {
                Data = gamePreviews,
                CurrentPage = page.Value,
                MaxPageSelectors = this.MaxPageSelectors,
                TotalPages = (int)Math.Ceiling((double)games.Count / this.PageSize)
            });
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