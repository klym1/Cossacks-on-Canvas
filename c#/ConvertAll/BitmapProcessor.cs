using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Common;

namespace ConvertAll
{
    internal class BitmapProcessor
    {
        private string BaseDirectory { get; set; }

        public BitmapProcessor(string baseDirectory)
        {
            BaseDirectory = baseDirectory;
        }

        public SpriteGrid RunAllSteps(string[] userLC)
        {
            var joinedBitmaps = CreateJoinedBitmaps(userLC);

            return CreateSpritesInImagesPng(joinedBitmaps);
        }

        public Sprite CreateJoinedBitmaps(string[] userLC)
        {
            var id = Int32.Parse(userLC[1]);
            var spriteName = userLC[2].ToUpperInvariant();
            var mirrorOffset = Int32.Parse(userLC[4]) * (-1);

            var sprite = new Sprite
            {
                Id = id,
                MirrorOffset = mirrorOffset,
                Name = spriteName,
                Bitmaps = Verify.JoinAllImagesAndAlphasFromDirectory(spriteName, BaseDirectory)
            };

            return sprite;
        }

        public SpriteGrid CreateSpritesInImagesPng(Sprite resultArrayOfJoinedBitmap)
        {
            var c = resultArrayOfJoinedBitmap.Bitmaps.Select(it => new B { Content = it }).ToArray();

            var maxHeight = c.Max(it => it.Content.Height);
            var maxWidth = resultArrayOfJoinedBitmap.MirrorOffset * 2;

            var bitmap = CreateSpriteBitmap(c, maxHeight, maxWidth, resultArrayOfJoinedBitmap.MirrorOffset);

            var spriteInfo = new SpriteGrid
            {
                Id = resultArrayOfJoinedBitmap.Id,
                GridBitmap = bitmap,
                UnitName = resultArrayOfJoinedBitmap.Name,
                SpriteHeight = maxHeight,
                SpriteWidth = maxWidth,
                NumberOfFrames = c.Length / 9,
                NumberOfDirections = 16
            };

            return spriteInfo;
        }

        private static Bitmap CreateSpriteBitmap(B[] c, int maxHeight, int maxWidth, int jjj)
        {
            var newimage = new Bitmap(maxWidth * 16, maxHeight * (c.Length / 9));

            var graphics = Graphics.FromImage(newimage);

            for (int z = 0; z < 9; z++)
            {
                for (int i = z, j = 0; i < c.Length; i += 9, j++)
                {
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