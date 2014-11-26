using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using Common;
using MDParser;

namespace ConvertAll
{
    internal class Program
    {

        private const string BaseDirectory =   @"..\..\..\..\images\";
        private const string OutputDirectory = @"..\..\..\..\images_png\";
        private const string SpritesDirectory = @"..\..\..\..\sprites_png\";

        private static ICollection<SpriteInfo> SpriteInfos { get; set; } 

        private static bool IsDebug { get; set; }

        private static readonly float[] dashValues = { 5, 5 };

        private readonly static Color color = Color.Red;

        private readonly static Brush brush = new SolidBrush(color);
        private readonly static Pen pen = new Pen(color);
        
        private readonly static Font font  = new Font("Verdana", 10);

        private static void Main(string [] args)
        {
//            var allDirectories = Directory
//                .GetDirectories(BaseDirectory)
//                .Where(it => it.Contains("GRE"));

            if (args.Length > 0)
            {
                IsDebug = args[0] == "--d";
            }

           // Verify.JoinAllImagesAndAlphasFromDirectory(Path.Combine(BaseDirectory, "GUSS.gp"), OutputDirectory);

            var unit = "GRE";

            var mdParser = new MDFileParser(Path.Combine(@"..\..\..\..\MD\", unit + ".md"));

            var j = mdParser.FindUserLCShadow();

            var joinedImagesOutput = j;

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

        private static void CreateSpritesInImagesPng(IList<string> directory)
        {
            var spriteName = directory[2].ToUpperInvariant();

            var mirrorOffset = Int32.Parse(directory[4])*(-1);

            var spritePath = Path.Combine(OutputDirectory, spriteName);

            var c = Directory.GetFiles(spritePath, "*.png").Select(it =>
            {
                var newi = (Bitmap) Image.FromFile(it);

                return new B
                {
                    Content = newi,
                };
            }).ToArray();
            
            var maxHeight = c.Max(it => it.Content.Height);
            
            var maxWidth = mirrorOffset * 2;
            var bitmap = CreateSpriteBitmap(c, maxHeight, maxWidth, mirrorOffset);
            
            if (!Directory.Exists(SpritesDirectory))
            {
                Directory.CreateDirectory(SpritesDirectory);
            }

            SaveBitmap(bitmap, Path.Combine(SpritesDirectory, spriteName + ".png"));
            
            var spriteInfo = new SpriteInfo()
            {
                UnitName = spriteName,
                SpriteHeight = maxHeight,
                SpriteWidth = maxWidth,
                NumberOfFrames = c.Length / 9,
                NumberOfDirections = 16
            };

            SpriteInfos.Add(spriteInfo);
        }
        
        private static void SaveInfo(ICollection<SpriteInfo> spriteInfo, string path)
        {
            File.WriteAllText(path, "var sprites = " + (new JavaScriptSerializer()).Serialize(spriteInfo));
        }
        
        private static Bitmap CreateSpriteBitmap(B [] c, int maxHeight, int maxWidth, int jjj)
        {
            var newimage = new Bitmap(maxWidth * 16, maxHeight * (c.Length / 9));

            var graphics = Graphics.FromImage(newimage);
            
            for (int z = 0; z < 9; z++)
            {
                for (int i = z, j = 0; i < c.Length; i += 9, j++)
                {
                    if (IsDebug)
                    {
                        graphics.FillRectangle(brush, z * maxWidth, maxHeight * j, c[i].Content.Width, c[i].Content.Height);
                    }

                    graphics.DrawImage(c[i].Content, z * maxWidth, maxHeight * j);
                    
                    if (z > 0 && z < 8)
                    {
                        DrawFlippedImage(c[i], maxWidth, maxHeight, graphics, z, j, jjj);
                    }
                } 
            }

            graphics.Save();

            return newimage;
        }

        private static void DrawFlippedImage(B c, int maxWidth, int maxHeight, Graphics graphics, int z, int j, int jjj)
        {
            TranslateAllPixelsToRight(c.Content, maxWidth, maxHeight, jjj);

            if (IsDebug)
            {
                graphics.FillRectangle(brush, (-z + 16) * maxWidth, maxHeight * j, c.Content.Width, c.Content.Height);
            }
            
            graphics.DrawImage(c.Content, (-z + 16) * maxWidth, maxHeight * j);
        }

        static void TranslateAllPixelsToRight(Bitmap image, int maxmwidth, int maxHeight, int jjj)
        {
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width,
              image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

            byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * image.Height];
            IntPtr scan0 = imageData.Scan0;

            Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);
            
            for (int i = 0; i < imageBytes.Length; i += 3)
            {
                //byte pixelR = imageBytes[i + 2];
              //  byte pixelG = imageBytes[i + 1];
                byte pixelB = imageBytes[i];
                
                
               // imageBytes[i + 2] = pixelR;
               // imageBytes[i + 1] = pixelG;
                imageBytes[i] = pixelB;
                
            }

            Marshal.Copy(imageBytes, 0, scan0, imageBytes.Length);

            image.UnlockBits(imageData);
        }

//        private static Bitmap TranslateAllPixelsToRight(Bitmap bitmap, int maxmwidth, int maxHeight, int jjj)
//        {
//            var newbitmap = new Bitmap(maxmwidth, maxHeight);
//
//            for (int j = 0; j < bitmap.Height; j++)
//                for (int i = 0; i < bitmap.Width; i++)
//                {
//                    newbitmap.SetPixel(2*jjj - i - 1, j, bitmap.GetPixel(i, j));
//                }
//
//            return newbitmap;
//        }
        
        private static void SaveBitmap(Bitmap bitmap, string path)
        {
            using (var newBitmap = new Bitmap(bitmap))
            {
                newBitmap.Save(path, ImageFormat.Png);
            }
        }
    }
}