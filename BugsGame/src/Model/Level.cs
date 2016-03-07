using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Level
    {
        public int id { get; set; }
        public string title { get; set; }
        public int seconds { get; set; }
        public string background { get; set; }
        public List<Bug> bugs { get; set; }
    }
}
