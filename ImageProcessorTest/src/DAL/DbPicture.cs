using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class DbPicture : IDbPictures
    {
        private ImageTestContext db;

        public DbPicture(ImageTestContext context)
        {
            db = context;
        }

        public Picture addPicture(Picture picture)
        {
            throw new NotImplementedException();
        }

        public Picture deletePicture(int id)
        {
            throw new NotImplementedException();
        }

        public Picture getPicture(int id)
        {
            throw new NotImplementedException();
        }

        public List<Picture> getPictureOfPage(int pageId)
        {
            throw new NotImplementedException();
        }

        public List<Picture> getPictures()
        {
            return db.Pictures.Select(p => new Picture {
                id = p.Id,
                description = p.Description,
                url = p.Url
            }).ToList();
        }

        public Picture updatePicture(Picture picture)
        {
            throw new NotImplementedException();
        }
    }
}
