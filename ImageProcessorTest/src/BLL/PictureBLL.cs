using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using DAL;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;

namespace BLL
{
    public class PictureBLL : IPictureBLL
    {
        private IDbPictures _pictureRepo;

        public PictureBLL(IDbPictures pictureRepo)
        {
            _pictureRepo = pictureRepo;
        }

        public Picture addPicture(Picture picture)
        {
            return _pictureRepo.addPicture(picture);
        }

        public Picture deletePicture(int id)
        {
            return _pictureRepo.deletePicture(id);
        }

        public Picture getPicture(int id)
        {
            return _pictureRepo.getPicture(id);
        }

        public List<Picture> getPictureOfPage(int pageId)
        {
            return _pictureRepo.getPictureOfPage(pageId);
        }

        public List<Picture> getPictures()
        {
            return _pictureRepo.getPictures();
        }

        public Picture updatePicture(Picture picture)
        {
            return _pictureRepo.updatePicture(picture);
        }

        public async Task<bool> uploadPicture(IFormFile value, string basePath) {
            var fileName = ContentDispositionHeaderValue
                .Parse(value.ContentDisposition)
                .FileName
                .Trim('"');// FileName returns "fileName.ext"(with double quotes) in beta 3


                var filePath = basePath + "\\wwwroot\\Content\\Pictures\\" + fileName;
                await value.SaveAsAsync(filePath);

            throw new NotImplementedException();
        }
    }
}
