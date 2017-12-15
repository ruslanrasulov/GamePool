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
    public class AdminController : Controller
    {
        private readonly IGameLogic gameLogic;
        private readonly ISystemRequirementsLogic systemRequirementsLogic;
        private readonly IGenreLogic genreLogic;
        private readonly IAvatarLogic avatarLogic;
        private readonly IImageLogic imageLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IUserLogic userLogic;
        private readonly IUserRoleLogic userRoleLogic;

        private readonly string imagePath;
        private readonly int pageSize;
        private readonly int maxPageSelectors;

        public AdminController(
            IGameLogic gameLogic,
            ISystemRequirementsLogic systemRequirementsLogic,
            IGenreLogic genreLogic,
            IAvatarLogic avatarLogic,
            IImageLogic imageLogic,
            IOrderLogic orderLogic,
            IUserLogic userLogic,
            IUserRoleLogic userRoleLogic)
        {
            this.gameLogic = gameLogic;
            this.systemRequirementsLogic = systemRequirementsLogic;
            this.genreLogic = genreLogic;
            this.avatarLogic = avatarLogic;
            this.imageLogic = imageLogic;
            this.orderLogic = orderLogic;
            this.userLogic = userLogic;
            this.userRoleLogic = userRoleLogic;

            this.imagePath = ConfigurationManager.AppSettings["VirtualImagePath"];
            this.pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            this.maxPageSelectors = int.Parse(ConfigurationManager.AppSettings["MaxPageSelectors"]);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public ActionResult UserList(int? pageNumber = 1)
        {
            var pagedUsers = this.userLogic.GetAll(pageNumber.Value, this.pageSize);
            var allRoles = this.userRoleLogic.GetAll().Select(r => r.Name);

            var userListItems = Mapper.Map<IEnumerable<User>, IEnumerable<UserListItemVM>>(
                pagedUsers.Data.Where(u => u.Name != User.Identity.Name));

            var pagedItems = new PagedItems<UserListItemVM>
            {
                Data = FillRolesIntoUser(userListItems, allRoles),
                CurrentPage = pageNumber.Value,
                MaxPageSelectors = this.maxPageSelectors,
                TotalPages = pagedUsers.Count
            };

            return View(pagedItems);
        }

        [HttpGet]
        public ActionResult Orders(int? pageNumber = 1)
        {
            var pagedOrders = this.orderLogic.GetAll(pageNumber.Value, this.pageSize);

            var pagedItems = new PagedItems<OrderListItemVM>
            {
                Data = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderListItemVM>>(pagedOrders.Data),
                CurrentPage = pageNumber.Value,
                MaxPageSelectors = this.maxPageSelectors,
                TotalPages = pagedOrders.Count
            };

            return View(pagedItems);
        }

        [HttpGet]
        public ActionResult AddGame()
        {
            return View(new CreateGameVM
            {
                MinimalSystemRequirements = new CreateSystemRequirementsVM(),
                RecommendedSystemRequirements = new CreateSystemRequirementsVM()
            });
        }

        [HttpPost]
        public ActionResult AddGame(CreateGameVM createGameVM)
        {
            if (createGameVM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var gameEntity = Mapper.Map<CreateGameVM, GameEntity>(createGameVM);
                var minimalSystemReq = Mapper.Map<CreateSystemRequirementsVM, SystemRequirements>(createGameVM.MinimalSystemRequirements);
                var recommendedSystemReq = Mapper.Map<CreateSystemRequirementsVM, SystemRequirements>(createGameVM.RecommendedSystemRequirements);

                if (gameLogic.Add(gameEntity))
                {
                    minimalSystemReq.GameId = gameEntity.Id;
                    recommendedSystemReq.GameId = gameEntity.Id;

                    if (systemRequirementsLogic.Add(minimalSystemReq) && 
                        systemRequirementsLogic.Add(recommendedSystemReq) &&
                        genreLogic.AddGenresByGameId(gameEntity.Id, createGameVM.GenreIds))
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

                            if (this.imageLogic.Add(newImage))
                            {
                                var path = Path.Combine(Server.MapPath(this.imagePath), image.FileName);

                                image.Save(path);

                                this.avatarLogic.SetForGame(gameEntity.Id, newImage.Id);
                            }
                        }

                        return RedirectToAction("Index", "Product");
                    }
                }

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View(createGameVM);
        }

        [HttpGet]
        public ActionResult GetGenresByNamePart(string name)
        {
            var genreEntities = this.genreLogic.GetByNamePart(name);

            var genres = Mapper.Map<IEnumerable<Genre>, IEnumerable<Select2GenreVM>>(genreEntities);

            var json = JsonConvert.SerializeObject(
                genres,
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return Content(json);
        }

        [HttpPost]
        public ActionResult GetGenresByIds(IEnumerable<int> ids)
        {
            var genreEntities = this.genreLogic.GetByIds(ids);
            var genres = Mapper.Map<IEnumerable<Genre>, IEnumerable<Select2GenreVM>>(genreEntities);

            var json = JsonConvert.SerializeObject(
                new { Genres = genres },
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return Content(json);
        }

        [HttpGet]
        public ActionResult EditGame(int id)
        {
            var gameEntity = this.gameLogic.GetById(id);

            if (gameEntity == null)
            {
                return HttpNotFound();
            }

            var gameForEdit = Mapper.Map<GameEntity, EditGameVM>(gameEntity);
            gameForEdit.GenreIds = this.genreLogic.GetByGameId(gameForEdit.Id).Select(g => g.Id);

            return View(gameForEdit);
        }

        [HttpPost]
        public ActionResult EditGame(EditGameVM editGameVM)
        {
            if (editGameVM == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                var gameEntity = Mapper.Map<EditGameVM, GameEntity>(editGameVM);

                if (this.gameLogic.Update(gameEntity) &&
                    this.genreLogic.UpdateGenresByGameId(gameEntity.Id, editGameVM.GenreIds) &&
                    this.systemRequirementsLogic.Update(gameEntity.MinimalSystemRequirements) &&
                    this.systemRequirementsLogic.Update(gameEntity.RecommendedSystemRequirements))
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
                        
                        if (this.imageLogic.Add(newImage))
                        {
                            var path = Path.Combine(Server.MapPath(this.imagePath), image.FileName);

                            image.Save(path);

                            this.avatarLogic.SetForGame(gameEntity.Id, newImage.Id);
                        }                     
                    }

                    return RedirectToAction("Details", "Product", new { id = gameEntity.Id });
                }
            }

            return View(editGameVM);
        }
        
        [HttpGet]
        public ActionResult AddRoleToUser(string username, string roleName)
        {
            var result = this.userRoleLogic.AddRoleToUser(username, roleName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RemoveRoleFromUser(string username, string roleName)
        {
            var result = this.userRoleLogic.RemoveRoleFromUser(username, roleName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<UserListItemVM> FillRolesIntoUser(IEnumerable<UserListItemVM> userListItemsVM, IEnumerable<string> allRoles)
        {
            foreach (var user in userListItemsVM)
            {
                user.CurrentRoles = this.userRoleLogic.GetByUserLogin(user.Name).Select(r => r.Name);
                user.AvailableRoles = allRoles.Except(user.CurrentRoles);

                yield return user;
            }
        }
    }
}