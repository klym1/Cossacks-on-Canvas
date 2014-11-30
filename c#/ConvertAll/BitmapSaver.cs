using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace ConvertAll
{
    public class BitmapSaver
    {
        private string SpritesDirectory { get; set; }

        public BitmapSaver(string spritesDirectory)
        {
            SpritesDirectory = spritesDirectory;
        }

        public void SaveJsonAndBitmaps(ConcurrentStack<SpriteGrid> grids)
        {
            if (!Directory.Exists(SpritesDirectory))
            {
                Directory.CreateDirectory(SpritesDirectory);
            }

            foreach (var spriteGrid in grids)
            {
                SaveBitmap(spriteGrid.GridBitmap, Path.Combine(SpritesDirectory, spriteGrid.UnitName + ".png"));
            }

            SaveInfo(grids, Path.Combine(SpritesDirectory, "sprites-info.json"));
        }

        private static void SaveInfo(IEnumerable<SpriteGrid> spriteInfo, string path)
        {
            File.WriteAllText(path, "var sprites = " + (new JavaScriptSerializer()).Serialize(spriteInfo.OrderBy(it => it.Id)));
        }

        private static void SaveBitmap(Bitmap bitmap, string path)
        {
            using (var newBitmap = new Bitmap(bitmap))
            {
                newBitmap.Save(path, ImageFormat.Png);
            }
        }   
    }
}