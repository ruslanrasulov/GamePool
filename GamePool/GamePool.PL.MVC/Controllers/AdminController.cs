using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper;
using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using GamePool.PL.MVC.Models.Admin;
using GamePool.PL.MVC.Models.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GamePool.PL.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IGameLogic _gameLogic;
        private readonly ISystemRequirementsLogic _systemRequirementsLogic;
        private readonly IGenreLogic _genreLogic;
        private readonly IImageLogic _imageLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly IUserLogic _userLogic;
        private readonly IUserRoleLogic _userRoleLogic;

        private readonly string _imagePath;
        private readonly int _pageSize;
        private readonly int _maxPageSelectors;

        public AdminController(
            IGameLogic gameLogic,
            ISystemRequirementsLogic systemRequirementsLogic,
            IGenreLogic genreLogic,
            IImageLogic imageLogic,
            IOrderLogic orderLogic,
            IUserLogic userLogic,
            IUserRoleLogic userRoleLogic)
        {
            _gameLogic = gameLogic;
            _systemRequirementsLogic = systemRequirementsLogic;
            _genreLogic = genreLogic;
            _imageLogic = imageLogic;
            _orderLogic = orderLogic;
            _userLogic = userLogic;
            _userRoleLogic = userRoleLogic;

            _imagePath = ConfigurationManager.AppSettings["VirtualImagePath"];
            _pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            _maxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public ActionResult UserList(int? pageNumber = 1)
        {
            var pagedUsers = _userLogic.GetAll(pageNumber.Value, _pageSize);
            var allRoles = _userRoleLogic.GetAll().Select(r => r.Name);

            var userListItems = Mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserListItemVm>>(
                pagedUsers.Data.Where(u => !string.Equals(u.Name, User.Identity.Name, StringComparison.InvariantCultureIgnoreCase)));

            var totalPages = (int)Math.Ceiling((double)pagedUsers.Count / _pageSize);

            var pagedItems = new PagedItems<UserListItemVm>
            {
                Data = FillRolesIntoUser(userListItems, allRoles),
                PageInfo = new PageInfo
                {
                    CurrentPage = pageNumber.Value,
                    StartIndex = GetStartIndex(pageNumber.Value, totalPages),
                    PageLinksCount = (totalPages < _maxPageSelectors) ? totalPages : _maxPageSelectors,
                    TotalPages = totalPages
                }
            };

            return View(pagedItems);
        }

        [HttpGet]
        public ActionResult Orders(int? pageNumber = 1)
        {
            var pagedOrders = _orderLogic.GetAll(pageNumber.Value, _pageSize);
            var totalPages = (int)Math.Ceiling((double)pagedOrders.Count / _pageSize);

            var pagedItems = new PagedItems<OrderListItemVm>
            {
                Data = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderListItemVm>>(pagedOrders.Data),
                PageInfo = new PageInfo
                {
                    CurrentPage = pageNumber.Value,
                    StartIndex = GetStartIndex(pageNumber.Value, totalPages),
                    PageLinksCount = (totalPages < _maxPageSelectors) ? totalPages : _maxPageSelectors,
                    TotalPages = totalPages
                }
            };

            return View(pagedItems);
        }

        [HttpGet]
        public ActionResult AddGame()
        {
            return View(new CreateGameVm
            {
                MinimalSystemRequirements = new CreateSystemRequirementsVm(),
                RecommendedSystemRequirements = new CreateSystemRequirementsVm()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGame(CreateGameVm createGameVm)
        {
            if (createGameVm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var gameEntity = Mapper.Map<CreateGameVm, GameEntity>(createGameVm);
                var minimalSystemReq = Mapper.Map<CreateSystemRequirementsVm, SystemRequirements>(createGameVm.MinimalSystemRequirements);
                var recommendedSystemReq = Mapper.Map<CreateSystemRequirementsVm, SystemRequirements>(createGameVm.RecommendedSystemRequirements);

                if (_gameLogic.Add(gameEntity))
                {
                    minimalSystemReq.GameId = gameEntity.Id;
                    recommendedSystemReq.GameId = gameEntity.Id;

                    if (_systemRequirementsLogic.Add(minimalSystemReq) && 
                        _systemRequirementsLogic.Add(recommendedSystemReq) &&
                        _genreLogic.AddGenresByGameId(gameEntity.Id, createGameVm.GenreIds))
                    {
                        var image = WebImage.GetImageFromRequest("game-avatar");

                        if (image != null)
                        {
                            var newImage = new ImageEntity
                            {
                                MimeType = image.ImageFormat,
                                Path = image.FileName,
                                AlternativeText = "Game Avatar"
                            };

                            if (_imageLogic.Add(newImage))
                            {
                                var path = Path.Combine(Server.MapPath(_imagePath), image.FileName);

                                image.Save(path);

                                _imageLogic.SetAvatarForGame(gameEntity.Id, newImage.Id);
                            }
                        }

                        return RedirectToAction("Index", "Product");
                    }
                }

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View(createGameVm);
        }

        [HttpGet]
        public ActionResult GetGenresByNamePart(string name)
        {
            var genreEntities = _genreLogic.GetByNamePart(name);

            var genres = Mapper.Map<IEnumerable<Genre>, IEnumerable<Select2GenreVm>>(genreEntities);

            var json = JsonConvert.SerializeObject(
                genres,
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return Content(json);
        }

        [HttpPost]
        public ActionResult GetGenresByIds(IEnumerable<int> ids)
        {
            var genreEntities = _genreLogic.GetByIds(ids);
            var genres = Mapper.Map<IEnumerable<Genre>, IEnumerable<Select2GenreVm>>(genreEntities);

            var json = JsonConvert.SerializeObject(
                new { Genres = genres },
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return Content(json);
        }

        [HttpGet]
        public ActionResult EditGame(int id)
        {
            var gameEntity = _gameLogic.GetById(id);

            if (gameEntity == null)
            {
                return HttpNotFound();
            }

            var gameForEdit = Mapper.Map<GameEntity, EditGameVm>(gameEntity);
            gameForEdit.GenreIds = _genreLogic.GetByGameId(gameForEdit.Id).Select(g => g.Id);

            return View(gameForEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGame(EditGameVm editGameVm)
        {
            if (editGameVm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var gameEntity = Mapper.Map<EditGameVm, GameEntity>(editGameVm);

                if (_gameLogic.Update(gameEntity) &&
                    _genreLogic.UpdateGenresByGameId(gameEntity.Id, editGameVm.GenreIds) &&
                    _systemRequirementsLogic.Update(gameEntity.MinimalSystemRequirements) &&
                    _systemRequirementsLogic.Update(gameEntity.RecommendedSystemRequirements))
                {
                    var image = WebImage.GetImageFromRequest("game-avatar");

                    if (image != null)
                    {
                        var newImage = new ImageEntity
                        {
                            MimeType = image.ImageFormat,
                            Path = image.FileName,
                            AlternativeText = "Game Avatar"
                        };
                        
                        if (_imageLogic.Add(newImage))
                        {
                            var path = Path.Combine(Server.MapPath(_imagePath), image.FileName);

                            image.Save(path);

                            _imageLogic.SetAvatarForGame(gameEntity.Id, newImage.Id);
                        }                     
                    }

                    return RedirectToAction("Details", "Product", new { id = gameEntity.Id });
                }
            }

            return View(editGameVm);
        }
        
        [HttpGet]
        public ActionResult AddRoleToUser(string username, string roleName)
        {
            var result = _userRoleLogic.AddRoleToUser(username, roleName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RemoveRoleFromUser(string username, string roleName)
        {
            var result = _userRoleLogic.RemoveRoleFromUser(username, roleName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<UserListItemVm> FillRolesIntoUser(IEnumerable<UserListItemVm> userListItemsVm, IEnumerable<string> allRoles)
        {
            foreach (var user in userListItemsVm)
            {
                user.CurrentRoles = _userRoleLogic.GetByUserLogin(user.Name).Select(r => r.Name);
                user.AvailableRoles = allRoles.Except(user.CurrentRoles);

                yield return user;
            }
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