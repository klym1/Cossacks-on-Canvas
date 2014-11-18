using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Common;

namespace ConvertAll
{
    internal class Program
    {

        private const string BaseDirectory =   @"..\..\..\..\images\";
        private const string OutputDirectory = @"..\..\..\..\images_png\";
        private const string SpritesDirectory = @"..\..\..\..\sprites_png\";

        private static ICollection<SpriteInfo> SpriteInfos { get; set; } 

        private static bool IsDebug { get; set; }

        private static void Main(string [] args)
        {
//            var allDirectories = Directory
//                .GetDirectories(BaseDirectory)
//                .Where(it => it.Contains("GRE"));

            if (args.Length > 0)
            {
                IsDebug = args[0] == "--d";
            }

            var joinedImagesOutput = Directory
               .GetDirectories(OutputDirectory)
               .Where(it => it.Contains("GRE"));

            if (!Directory.Exists(OutputDirectory))
            {
                Directory.CreateDirectory(OutputDirectory);
            }
  
            SpriteInfos = new Collection<SpriteInfo>();

            foreach (var directory in joinedImagesOutput)
            {
                CreateSpritesInImagesPng(directory);
            }

            SaveInfo(SpriteInfos, Path.Combine(SpritesDirectory, "sprites-info.json"));
        }

        private static void CreateSpritesInImagesPng(string directory)
        {
            var c = Directory.GetFiles(directory, "*.png").Select(it =>
            {
                var newi = (Bitmap) Image.FromFile(it);

                return new B
                {
                    Content = newi,
                    Width = newi.Width,
                    Height = newi.Height
                };
            }).ToList();

            var maxWidth = c.Max(it => it.Width);
            var maxHeight = c.Max(it => it.Height);

            var bitmap = CreateSpriteBitmap(c, maxHeight, maxWidth);

            var unitName = directory.Split('\\')[directory.Count(it => it == '\\')];

            if (!Directory.Exists(SpritesDirectory))
            {
                Directory.CreateDirectory(SpritesDirectory);
            }

            SaveBitmap(bitmap, Path.Combine(SpritesDirectory, unitName + ".png"));
            
            var spriteInfo = new SpriteInfo()
            {
                UnitName = unitName,
                SpriteHeight = maxHeight,
                SpriteWidth = maxWidth,
                NumberOfFrames = c.Count / 9,
                NumberOfDirections = 16
            };

            SpriteInfos.Add(spriteInfo);
        }

        private static void SaveInfo(ICollection<SpriteInfo> spriteInfo, string path)
        {
            File.WriteAllText(path, "var sprites = " + (new JavaScriptSerializer()).Serialize(spriteInfo));
        }
        
        private static Bitmap CreateSpriteBitmap(List<B> c, int maxHeight, int maxWidth)
        {
            var newimage = new Bitmap(maxWidth * 16, maxHeight * (c.Count / 9));

            var graphics = Graphics.FromImage(newimage);

            float[] dashValues = { 5, 5 };
            
            var pen = new Pen(Color.Red);

            pen.DashPattern = dashValues;

            for (int z = 0; z < 9; z++)
            {
                for (int i = z, j = 0; i < c.Count; i += 9, j++)
                {
                    if (IsDebug)
                    {
                        graphics.DrawRectangle(pen, z*maxWidth, maxHeight*j, c[i].Width, c[i].Height);
                    }

                    graphics.DrawImage(c[i].Content, z * maxWidth, maxHeight * j);
                    
                    if (z > 0 && z < 8)
                    {
                        DrawFlippedImage(c, maxHeight, maxWidth, i, graphics, z, j);
                    }
                }

               
            }

            graphics.Save();

            return newimage;
        }

        private static void DrawFlippedImage(List<B> c, int maxHeight, int maxWidth, int i, Graphics graphics, int z, int j)
        {
            float[] dashValues = { 5, 5 };

            var pen = new Pen(Color.Red);

            if (IsDebug)
            {
                graphics.DrawRectangle(pen, (-z + 16) * maxWidth, maxHeight * j, c[i].Width, c[i].Height);
            }

            c[i].Content.RotateFlip(RotateFlipType.RotateNoneFlipX);

            graphics.DrawImage(c[i].Content, (-z + 16) * maxWidth + (maxWidth - c[i].Width), maxHeight * j);
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