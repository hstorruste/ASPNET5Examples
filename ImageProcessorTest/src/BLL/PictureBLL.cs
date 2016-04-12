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
        private FileHandler _fileHandler;

        public PictureBLL(IDbPictures pictureRepo, FileHandler fileHandler)
        {
            _pictureRepo = pictureRepo;
            _fileHandler = fileHandler;
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
            //return _pictureRepo.getPictures();

            
            return _fileHandler.getPictures();
        }

        public Picture updatePicture(Picture picture)
        {
            return _pictureRepo.updatePicture(picture);
        }

        public async Task<bool> uploadPicture(ICollection<IFormFile> files, string basePath) {

            //return await _fileHandler.saveSmallPicture(files, basePath);

            //return await _fileHandler.savePicture(files, basePath);
            await _fileHandler.saveImageToBlob(files);
            return true;
        }
    }
}
