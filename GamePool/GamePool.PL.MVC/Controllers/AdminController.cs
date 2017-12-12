using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public AdminController(
            IGameLogic gameLogic,
            ISystemRequirementsLogic systemRequirementsLogic,
            IGenreLogic genreLogic)
        {
            this.gameLogic = gameLogic;
            this.systemRequirementsLogic = systemRequirementsLogic;
            this.genreLogic = genreLogic;
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
            return View();
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
                    return RedirectToAction("Details", "Product", new { id = gameEntity.Id });
                }
            }

            return View(editGameVM);
        }
    }
}