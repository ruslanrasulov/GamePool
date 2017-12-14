using System.Configuration;
using System.IO;
using System.Web.Mvc;
using GamePool.BLL.LogicContracts;

namespace GamePool.PL.MVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageLogic imageLogic;
        private readonly string imagePath;

        public ImageController(IImageLogic imageLogic)
        {
            this.imageLogic = imageLogic;

            this.imagePath = ConfigurationManager.AppSettings["VirtualImagePath"];
        }

        public ActionResult GetImageById(int id)
        {
            var image = this.imageLogic.GetById(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(Server.MapPath(this.imagePath), image.Path);

            return File(path, image.MimeType);
        }
    }
}