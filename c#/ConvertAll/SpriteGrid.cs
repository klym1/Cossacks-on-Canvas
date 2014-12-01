using System.Drawing;
using System.Web.Script.Serialization;

namespace ConvertAll
{
    public struct SpriteGrid
    {
        public int Id { get; set; }
        public int SpriteHeight { get; set; }
        public int SpriteWidth { get; set; }
        public int NumberOfFrames { get; set; }
        public int NumberOfDirections { get; set; }
        public string UnitName { get; set; }

        [ScriptIgnore]
        public Bitmap GridBitmap { get; set; }

        public int XSymmetry { get; set; }
        public int YSymmetry { get; set; }
    }
}