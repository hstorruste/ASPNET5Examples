using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public interface IPictureBLL
    {
        List<Picture> getPictures();
        List<Picture> getPictureOfPage(int pageId);
        Picture getPicture(int id);
        Picture addPicture(Picture picture);
        Picture updatePicture(Picture picture);
        Picture deletePicture(int id);
    }
}
