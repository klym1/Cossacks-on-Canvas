using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;

namespace Common
{
    public static class Verify
    {
        public static string GetUnitNameFromGPPath(string path)
        {
            var spriteMatch = Regex.Match(path, @"[^\\]+(?=.gp$)");

            return spriteMatch.Success ? spriteMatch.Value : null;
        }

        public static void JoinAllImagesAndAlphasFromDirectory(string gpDirectory, string outputDirectory)
        {
            var unitName = GetUnitNameFromGPPath(gpDirectory);
            
            if(unitName == null) throw new Exception("Required directory with *.gp name not found");

            var lstFile = String.Format("{0}\\{1}.lst", gpDirectory, unitName);

            if(!File.Exists(lstFile)) throw new FileNotFoundException(lstFile);
            
            foreach (var file in File.ReadAllLines(lstFile))
            {
                var imagePath = String.Format("{0}\\{1}.bmp", gpDirectory, file);
                var alphaPath = String.Format("{0}\\a{1}.bmp", gpDirectory, file);

                if (!File.Exists(imagePath)) throw new FileNotFoundException(imagePath);
                if (!File.Exists(alphaPath)) throw new FileNotFoundException(alphaPath);

                var spriteInfo = new SpriteInfo
                {
                    ImagePath = imagePath,
                    AlphaPath = alphaPath,
                    SpriteName = unitName,
                    File = file
                };

                CreateAlphaFromPath(spriteInfo);
                CreateImageFromPath(spriteInfo);
                
                SaveNewSprite(spriteInfo, outputDirectory);
            }  
        }

        private static void SaveNewSprite(SpriteInfo spriteInfo, string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory + spriteInfo.SpriteName))
            {
                Directory.CreateDirectory(outputDirectory + spriteInfo.SpriteName);
            }

            var newBitmap = Helper.JoinImageAndAlpha(spriteInfo.ImageBitmap, spriteInfo.AlphaBitmap);
            newBitmap.Save(outputDirectory + spriteInfo.SpriteName + "//" + spriteInfo.File + ".png", ImageFormat.Png);
        }


        private static void CreateAlphaFromPath(SpriteInfo spriteInfo)
        {
            try
            {
                spriteInfo.AlphaBitmap = new Bitmap(spriteInfo.AlphaPath);
            }
            catch (Exception)
            {
                throw new Exception("Alpha hasn't created successfully");
            }
        }

        private static void CreateImageFromPath(SpriteInfo spriteInfo)
        {
            try
            {
                spriteInfo.ImageBitmap = new Bitmap(spriteInfo.ImagePath);
            }
            catch (Exception)
            {
                throw new Exception("Image hasn't created successfully");
            }
        }
    }
}
