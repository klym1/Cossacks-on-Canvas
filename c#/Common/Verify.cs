using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Common
{
    public static class Verify
    {
        public static IList<Bitmap> JoinAllImagesAndAlphasFromDirectory(string spriteName, string baseDirectory)
        {
            var gpDirectory = Path.Combine(baseDirectory, spriteName + ".gp");

            var lstFile = String.Format("{0}\\{1}.lst", gpDirectory, spriteName);

            if(!File.Exists(lstFile)) throw new FileNotFoundException(lstFile);

            var lstFileContents = File.ReadAllLines(lstFile);

            var result = new Bitmap[lstFileContents.Length];
            
            for (int i = 0; i < lstFileContents.Length; i++)
            {
                var file = lstFileContents[i];

                var imagePath = String.Format("{0}\\{1}.bmp", gpDirectory, file);
                var alphaPath = String.Format("{0}\\a{1}.bmp", gpDirectory, file);

                if (!File.Exists(imagePath)) throw new FileNotFoundException(imagePath);
                if (!File.Exists(alphaPath)) throw new FileNotFoundException(alphaPath);

                var spriteInfo = new SpriteInfo
                {
                    ImagePath = imagePath,
                    AlphaPath = alphaPath,
                    SpriteName = spriteName,
                    File = file
                };

                CreateAlphaFromPath(spriteInfo);
                CreateImageFromPath(spriteInfo);
                
                result[i] = Helper.JoinImageAndAlpha(spriteInfo.ImageBitmap, spriteInfo.AlphaBitmap);
            }

            return result;
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
