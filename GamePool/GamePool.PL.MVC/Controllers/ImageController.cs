using GamePool.BLL.LogicContracts;
using GamePool.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GamePool.PL.MVC.Controllers
{
    public class ImageController : Controller
    {
        private IImageLogic imageLogic;

        public ImageController(IImageLogic imageLogic)
        {
            this.imageLogic = imageLogic;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            WebImage image = WebImage.GetImageFromRequest();

            if (image != null)
            {
                ImageEntity imageEntity = new ImageEntity
                {
                    Content = image.GetBytes(),
                    AlternativeText = "Some image",
                    MimeType = "image/jpeg"
                };

                this.imageLogic.Add(imageEntity);

                ViewBag.Id = imageEntity.Id;
            }

            return View();
        }

        public ActionResult Get(int id)
        {
            ImageEntity imageEntity = this.imageLogic.GetById(id);

            if (imageEntity != null)
            {
                return this.File(imageEntity.Content, imageEntity.MimeType);
            }

            return this.HttpNotFound();
        }

        public ActionResult Getting(int id)
        {
            if (this.imageLogic.Remove(id))
            {
                return this.View("Index");
            }

            return new EmptyResult();
        }
    }
}