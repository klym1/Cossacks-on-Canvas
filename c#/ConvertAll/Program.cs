using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private static readonly float[] dashValues = { 5, 5 };

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
         

            var joinedImagesOutput = Directory
               .GetDirectories(OutputDirectory)
               .Where(it => it.Contains("GUS"));

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
                };
            }).ToArray();

           
            var maxHeight = c.Max(it => it.Content.Height);
            
          //  var ExtendedBitmaps = GetExtendedBitmaps(c, maxWidth, maxHeight);

            int jjj = 62;

            if (directory.Contains("SWRH"))
            {
                jjj = 92;
            } else if (directory.Contains("GUSH"))
            {
                jjj = 95;
            }
            else
            {
                jjj = 65;
            }
            
            var maxWidth = jjj * 2;
           var bitmap = CreateSpriteBitmap(c, maxHeight, maxWidth, jjj);

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
            
            var pen = new Pen(Color.Green);

            pen.DashPattern = dashValues;

            var brush = new SolidBrush(pen.Color);
            var brush2 = new SolidBrush(Color.Red);
            var brush3 = new SolidBrush(Color.Orange);

            var font = new Font("Verdana", 10);

            for (int z = 0; z < 9; z++)
            {
                for (int i = z, j = 0; i < c.Length; i += 9, j++)
                {
//                    var l = ContentStartPixelIndexByXFromLeft(c[i].Content);
//                    var r = ContentStartPixelIndexByXFromRight(c[i].Content);
                    
                    if (IsDebug)
                    {
                      //  graphics.FillRectangle(true ? brush2 : brush3, z * maxWidth, maxHeight * j, c[i].Content.Width, c[i].Content.Height);
                       // graphics.FillRectangle(brush, z * maxWidth + l, maxHeight * j, r - l + 1, c[i].Content.Height);
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
            c.Content = TranslateAllPixelsToRight(c.Content, maxWidth, maxHeight, jjj);

            if (IsDebug)
            {

              //  graphics.FillRectangle(brush2, (-z + 16) * maxWidth, maxHeight * j, c.Content.Width, c.Content.Height);
              //  graphics.FillRectangle(brush, (-z + 16) * maxWidth + l, maxHeight * j, r - l + 1, c.Content.Height);
            }
            
            graphics.DrawImage(c.Content, (-z + 16) * maxWidth, maxHeight * j);
        }
        
        private static Bitmap TranslateAllPixelsToRight(Bitmap bitmap, int maxmwidth, int maxHeight, int jjj)
        {
            var newbitmap = new Bitmap(maxmwidth, maxHeight);
            
            for (int j = 0; j < bitmap.Height; j++)
                    for (int i = 0; i < bitmap.Width; i++)
                {

                        newbitmap.SetPixel(2*jjj - i - 1, j,  bitmap.GetPixel(i, j));   
                }

            return newbitmap;
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