using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Model;

namespace UnitTest.Stubs
{
    public class DbPictureStub : IDbPictures
    {
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
                },
                new Picture
                {
                    id =2,
                    url= "www.2.no",
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
