using System.Configuration;
using System.IO;
using System.Web.Mvc;
using GamePool.BLL.LogicContracts;

namespace GamePool.PL.MVC.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageLogic _imageLogic;
        private readonly string _imagePath;

        public ImageController(IImageLogic imageLogic)
        {
            _imageLogic = imageLogic;

            _imagePath = ConfigurationManager.AppSettings["VirtualImagePath"];
        }

        public ActionResult GetImageById(int id)
        {
            var image = _imageLogic.GetById(id);

            if (image == null)
            {
                return HttpNotFound();
            }

            var path = Path.Combine(Server.MapPath(_imagePath), image.Path);

            return File(path, image.MimeType);
        }
    }
}