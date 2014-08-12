using System.Drawing;

namespace Common
{
    public class Helper
    {
        public static Bitmap JoinImageAndAlpha(Bitmap image, Bitmap alpha)
        {
            var resultBitMap = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < image.Width - 1; i++)
                for (int j = 0; j < image.Height - 1; j++)
                {
                    var oldPixel = image.GetPixel(i, j);
                    var oldPixela = alpha.GetPixel(i, j);

                    var newColor = Color.FromArgb(oldPixela.G, oldPixel.R, oldPixel.G, oldPixel.B);

                    resultBitMap.SetPixel(i, j, newColor);
                }

            return resultBitMap;
        }

        public static Image GetMirrorReflectionByX(Bitmap image, int maxWidth, int maxHeight)
        {
            var bigImage = new Bitmap(maxWidth, maxHeight);

            Graphics graphics = Graphics.FromImage(bigImage);

            

          for (int i = 0; i < image.Width/2 - 1; i++)
                for (int j = 0; j < image.Height - 1; j++)
                {
                    var l = image.GetPixel(i, j);
                    var r = image.GetPixel(image.Width - 1 - i, j);

                    image.SetPixel(i, j, r);
                    image.SetPixel(image.Width - 1 - i, j, l);
                }

          graphics.DrawImage(image, new Point(maxWidth - image.Width,0));

            graphics.Save();

            return bigImage;
        }
    }
}