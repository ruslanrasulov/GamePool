using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Product;
using GamePool.PL.MVC.Models.Search;
using GamePool.PL.MVC.Models.Shared;

namespace GamePool.PL.MVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly int PageSize;
        private readonly int MaxPageSelectors;
        private readonly IGameLogic gameLogic;

        public SearchController(IGameLogic gameLogic)
        {
            this.PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            this.MaxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);
            this.gameLogic = gameLogic;
        }

        public ActionResult Index(SearchResultsVM searchResultsVM)
        {
            var parameters = Mapper.Map<SearchParametersVM, SearchParameters>(searchResultsVM.Parameters);
            parameters.PageSize = this.PageSize;

            var foundGames = this.gameLogic.Search(parameters);
            var mappedGames = Mapper.Map<IEnumerable<GameEntity>, IEnumerable<GamePreviewVM>>(foundGames.Data);

            if (searchResultsVM.Items == null)
            {
                searchResultsVM.Items = new PagedItems<GamePreviewVM>();
                searchResultsVM.Items.MaxPageSelectors = this.MaxPageSelectors;
            }

            searchResultsVM.Items.Data = mappedGames;
            searchResultsVM.Items.TotalPages = (int)Math.Ceiling((double)foundGames.Count / this.PageSize);
            searchResultsVM.Items.CurrentPage = searchResultsVM.Parameters.PageNumber;

            return View(searchResultsVM);
        }

        public ActionResult SearchInput()
        {
            return PartialView("~/Views/Shared/_SearchInputPartial.cshtml", new SearchResultsVM { Parameters = new SearchParametersVM() });
        }
    }
}