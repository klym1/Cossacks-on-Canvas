using System.Collections.Generic;
using System.Drawing;

namespace ConvertAll
{
    public class Sprite
    {
        public string Name { get; set; }
        public IList<Bitmap> Bitmaps { get; set; }
        public int Id { get; set; }
        public int MirrorOffsetX { get; set; }
        public int MirrorOffsetY { get; set; }
    }
}