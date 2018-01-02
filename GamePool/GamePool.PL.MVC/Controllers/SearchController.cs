using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly IGameLogic _gameLogic;

        private readonly int _pageSize;
        private readonly int _maxPageSelectors;

        public SearchController(IGameLogic gameLogic)
        {
            _pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            _maxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);
            _gameLogic = gameLogic;
        }

        [HttpGet]
        public ActionResult Index(SearchResultsVm searchResultsVm)
        {
            var parameters = Mapper.Map<SearchParametersVm, SearchParameters>(searchResultsVm.Parameters);
            parameters.PageSize = _pageSize;

            var foundGames = _gameLogic.Search(parameters);
            var mappedGames = Mapper.Map<IEnumerable<GameEntity>, IEnumerable<GamePreviewVm>>(foundGames.Data);

            var totalPages = (int)Math.Ceiling((double)foundGames.Count / _pageSize);

            if (searchResultsVm.Items == null)
            {
                searchResultsVm.Items = new PagedItems<GamePreviewVm>()
                {
                    PageInfo = new PageInfo
                    {
                        PageLinksCount = (totalPages < _maxPageSelectors) ? totalPages : _maxPageSelectors,
                        TotalPages = totalPages
                    }
                };
            }

            searchResultsVm.Items.Data = mappedGames;
            searchResultsVm.Items.PageInfo.CurrentPage = searchResultsVm.Parameters.PageNumber;
            searchResultsVm.Items.PageInfo.StartIndex = GetStartIndex(searchResultsVm.Parameters.PageNumber, totalPages);
            
            return View(searchResultsVm);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult SearchInput()
        {
            return PartialView("~/Views/Shared/_SearchInputPartial.cshtml", new SearchResultsVm { Parameters = new SearchParametersVm() });
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