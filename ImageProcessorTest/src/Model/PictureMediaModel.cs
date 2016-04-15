using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Model
{
    public class PictureMediaModel
    {
        public ICollection<IFormFile> value { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }
}
