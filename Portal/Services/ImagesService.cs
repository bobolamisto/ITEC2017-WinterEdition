using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Services
{
    public class ImagesService
    {
        private readonly PortalDbContext _context;

        public ImagesService(PortalDbContext context)
        {
            _context = context;
        }
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

        public FileStreamResult ViewImage(int id)
        {
            Models.Image image = _context.Images.FirstOrDefault(m => m.Id == id);

            MemoryStream ms = new MemoryStream(image.Data);

            return new FileStreamResult(ms, image.ContentType);
        }
    }
}
