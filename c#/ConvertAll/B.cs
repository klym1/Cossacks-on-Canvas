using System.Diagnostics;
using System.Drawing;

namespace ConvertAll
{
    [DebuggerDisplay("{Width} {Height}")]
    public class B
    {
        public Bitmap Content { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}