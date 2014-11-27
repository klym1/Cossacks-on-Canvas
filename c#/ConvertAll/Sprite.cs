using System.Collections.Generic;
using System.Drawing;

namespace ConvertAll
{
    public class Sprite
    {
        public string Name { get; set; }
        public IList<Bitmap> Bitmaps { get; set; }
        public int MirrorOffset { get; set; }
    }
}