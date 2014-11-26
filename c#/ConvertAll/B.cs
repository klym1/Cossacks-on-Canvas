using System.Diagnostics;
using System.Drawing;

namespace ConvertAll
{
    [DebuggerDisplay("{Content.Width} {Content.Height}")]
    public class B
    {
        public Bitmap Content { get; set; }
    }
}