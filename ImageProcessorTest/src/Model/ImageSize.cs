using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class ImageSize
    {
        public Sizes Size { get; set; }
        public int width { get; set; }
    }

    public enum Sizes
    {
        thumb = 1,
        small,
        medium,
        large,
        original
    }
}
