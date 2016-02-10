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
            return new List<Picture>()
            {
                new Picture
                {
                    id =1,
                    url= "www.1.no",
                    description = "Dette er en test!"
                }
            };
        }

        public Picture updatePicture(Picture picture)
        {
            throw new NotImplementedException();
        }
    }
}
