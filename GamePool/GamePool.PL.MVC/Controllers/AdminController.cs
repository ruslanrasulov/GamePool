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
        private readonly string imagePath;
        
        public AdminController(
            IGameLogic gameLogic,
            ISystemRequirementsLogic systemRequirementsLogic,
            IGenreLogic genreLogic,
            IAvatarLogic avatarLogic,
            IImageLogic imageLogic)
        {
            this.gameLogic = gameLogic;
            this.systemRequirementsLogic = systemRequirementsLogic;
            this.genreLogic = genreLogic;
            this.avatarLogic = avatarLogic;
            this.imageLogic = imageLogic;

            this.imagePath = ConfigurationManager.AppSettings["VirtualImagePath"];
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public ActionResult UserList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Orders()
        {
            return View();
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

                        return RedirectToAction("Index");
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
    }
}