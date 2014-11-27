﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Common;
using MDParser;

namespace ConvertAll
{
    internal class Program
    {
        private const string BaseDirectory = @"..\..\..\..\images\";
        private const string MDDirectory = @"..\..\..\..\MD\";
//        private const string OutputDirectory = @"..\..\..\..\images_png\";
        private const string SpritesDirectory = @"..\..\..\..\sprites_png\";

        private static bool IsDebug { get; set; }

        private readonly static Color color = Color.Red;

        private readonly static Brush brush = new SolidBrush(color);

        private static void Main(string [] args)
        {
            if (args.Length > 0)
            {
                IsDebug = args[0] == "--d";
            }

            const string unit = "SWR";

            var sw = Stopwatch.StartNew();

            var mdParser = new MdFileParser(Path.Combine(MDDirectory, unit + ".md"));
            
            var userLCs = mdParser.FindUserLCShadow();

            var results = new ConcurrentStack<SpriteGrid>();

            Parallel.ForEach(userLCs, 
                new ParallelOptions()
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                }, userLCId =>
            {
                var joinedBitmaps = CreateJoinedBitmaps(userLCId);

                var grid = CreateSpritesInImagesPng(joinedBitmaps);

                results.Push(grid);
                
                Debug.WriteLine(userLCId[1]);
                Console.WriteLine(userLCId[1]);
            });

            SaveJsonAndBitmaps(results);

            sw.Stop();

            var elapsed = sw.Elapsed;
        }

        private static void SaveJsonAndBitmaps(ConcurrentStack<SpriteGrid> grids)
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

        private static void SaveInfo(ConcurrentStack<SpriteGrid> spriteInfo, string path)
        {
            File.WriteAllText(path, "var sprites = " + (new JavaScriptSerializer()).Serialize(spriteInfo));
        }

        private static void SaveBitmap(Bitmap bitmap, string path)
        {
            using (var newBitmap = new Bitmap(bitmap))
            {
                newBitmap.Save(path, ImageFormat.Png);
            }
        }

        private static Sprite CreateJoinedBitmaps(string[] userLC)
        {
          //  var sprites = new Sprite[userLC.Length];
            
          //  for (int i = 0; i < userLCs.Length; i++)
           // {
             //   var userLC = userLCs[i];
                
                var spriteName = userLC[2].ToUpperInvariant();
                var mirrorOffset = Int32.Parse(userLC[4]) * (-1);

               var sprite = new Sprite
                {
                    MirrorOffset = mirrorOffset,
                    Name = spriteName,
                    Bitmaps = Verify.JoinAllImagesAndAlphasFromDirectory(spriteName, BaseDirectory)
                };
         //   }

            return sprite;
        }

        private static SpriteGrid CreateSpritesInImagesPng(Sprite resultArrayOfJoinedBitmap)
        {
//            var result = new Collection<SpriteGrid>();

            // 4 - 6 elems
            //foreach (var resultArrayOfJoinedBitmap in resultArrayOfJoinedBitmaps)
            //{
                var c = resultArrayOfJoinedBitmap.Bitmaps.Select(it => new B { Content = it }).ToArray();

                var maxHeight = c.Max(it => it.Content.Height);
                var maxWidth = resultArrayOfJoinedBitmap.MirrorOffset * 2;

                var bitmap = CreateSpriteBitmap(c, maxHeight, maxWidth, resultArrayOfJoinedBitmap.MirrorOffset);
                
                var spriteInfo = new SpriteGrid
                {
                    GridBitmap = bitmap,
                    UnitName = resultArrayOfJoinedBitmap.Name,
                    SpriteHeight = maxHeight,
                    SpriteWidth = maxWidth,
                    NumberOfFrames = c.Length / 9,
                    NumberOfDirections = 16
                };
                
//                result.Add(spriteInfo);
            //}

            return spriteInfo;

        }
        
        private static Bitmap CreateSpriteBitmap(B [] c, int maxHeight, int maxWidth, int jjj)
        {
            var newimage = new Bitmap(maxWidth * 16, maxHeight * (c.Length / 9));

            var graphics = Graphics.FromImage(newimage);
            var sw = Stopwatch.StartNew();
            for (int z = 0; z < 9; z++)
            {
                for (int i = z, j = 0; i < c.Length; i += 9, j++)
                {
                    if (IsDebug)
                    {
                       // graphics.FillRectangle(brush, z * maxWidth, maxHeight * j, c[i].Content.Width, c[i].Content.Height);
                    }
                    var sw3 = Stopwatch.StartNew();
                    graphics.DrawImage(c[i].Content, z * maxWidth, maxHeight * j);
                   
                    if (z > 0 && z < 8)
                    {
                       DrawFlippedImage(c[i], maxWidth, maxHeight, graphics, z, j, jjj);
                    }

                    var ggg3 = sw3.Elapsed;
                } 
            }

            graphics.Save();
            var ggg = sw.Elapsed;
            return newimage;
        }

        private static void DrawFlippedImage(B c, int maxWidth, int maxHeight, Graphics graphics, int z, int j, int jjj)
        {
            c.Content = TranslateAllPixelsToRight(c.Content, maxWidth, maxHeight, jjj);

            if (IsDebug)
            {
              // graphics.FillRectangle(brush, (-z + 16) * maxWidth, maxHeight * j, c.Content.Width, c.Content.Height);
            }
            
            graphics.DrawImage(c.Content, (-z + 16) * maxWidth, maxHeight * j);
        }

        static Bitmap TranslateAllPixelsToRight(Bitmap image, int maxmwidth, int maxHeight, int jjj)
        {
            var newBitmap = new Bitmap(maxmwidth, maxHeight, image.PixelFormat);

            BitmapData imageData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width,
              newBitmap.Height), ImageLockMode.ReadWrite, newBitmap.PixelFormat);

            byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * newBitmap.Height];
            IntPtr scan0 = imageData.Scan0;

            Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);

            var oldImageBytes = Helper.getImageBytes(image);

            for (int i = 0; i < newBitmap.Width; i++)
                for (int j = 0; j < newBitmap.Height; j++)
                {
                    if (i < image.Width && j < image.Height)
                    {
                        var c = Helper.CartesianToArrayPosition(i, j, image.Width); 

                        var k = Helper.CartesianToArrayPosition(2 * jjj - 1 - i, j, newBitmap.Width);

                        imageBytes[k + 3] = oldImageBytes[c + 3];
                        imageBytes[k + 2] = oldImageBytes[c + 2];
                        imageBytes[k + 1] = oldImageBytes[c + 1];
                        imageBytes[k + 0] = oldImageBytes[c + 0];
                    }
                }
            
            Marshal.Copy(imageBytes, 0, scan0, imageBytes.Length);

            newBitmap.UnlockBits(imageData);

            return newBitmap;
        }  
    }
}