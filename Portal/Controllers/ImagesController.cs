using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Portal.Controllers
{
    public class ImagesController : Controller
    {
        private readonly PortalDbContext _context;

        public ImagesController(PortalDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
                List<int> iamgeIds = _context.Images.Select(m => m.Id).ToList();
                return View(iamgeIds);
        }

        [HttpPost]
        public int UploadImage(IList<IFormFile> files)
        {
            
            IFormFile uploadedImage = files.FirstOrDefault();
            Models.Image imageEntity = null;
            if (uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    imageEntity = new Models.Image()
                    {
                        Name = uploadedImage.Name,
                        Data = ms.ToArray(),
                        Width = image.Width,
                        Height = image.Height,
                        ContentType = uploadedImage.ContentType
                    };

                    _context.Images.Add(imageEntity);

                    _context.SaveChanges();
            }

            return imageEntity.Id;
        }

        [HttpGet]
        public FileStreamResult ViewImage(int id)
        {
                Models.Image image = _context.Images.FirstOrDefault(m => m.Id == id);

                MemoryStream ms = new MemoryStream(image.Data);

                return new FileStreamResult(ms, image.ContentType);
        }
    }
}