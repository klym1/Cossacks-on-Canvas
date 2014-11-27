using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Common
{
    public class Helper
    {
        public static byte[] getImageBytes(Bitmap image)
        {
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width,
                 image.Height), ImageLockMode.ReadWrite, image.PixelFormat);

            byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * image.Height];
            IntPtr scan0 = imageData.Scan0;

            Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);

            image.UnlockBits(imageData);

            return imageBytes;
        }
        
        public static int CartesianToArrayPosition(int x, int y, int width)
        {
            var result = y * width + x;

            return result * 4;
        } 

        public static Bitmap JoinImageAndAlpha(Bitmap image, Bitmap alpha)
        {
            var resultBitMap = new Bitmap(image.Width, image.Height);

            var imageBytes = getImageBytes(image);
            var alhpaBytes = getImageBytes(alpha);

            BitmapData imageData = resultBitMap.LockBits(new Rectangle(0, 0, resultBitMap.Width,
             resultBitMap.Height), ImageLockMode.ReadWrite, resultBitMap.PixelFormat);

            byte[] resultImageBytes = new byte[Math.Abs(imageData.Stride) * resultBitMap.Height];
            IntPtr scan0 = imageData.Scan0;

            Marshal.Copy(scan0, resultImageBytes, 0, resultImageBytes.Length);

            int i = 0;
            int j = 0;
            int z = 0;
            for (; i < resultImageBytes.Length ; i+=4, j+=3, z++)
            {
                resultImageBytes[i + 3] = alhpaBytes[z];
                resultImageBytes[i + 1] = imageBytes[j+1];
                resultImageBytes[i + 2] = imageBytes[j+2];
                resultImageBytes[i + 0] = imageBytes[j];
            }
            
            Marshal.Copy(resultImageBytes, 0, scan0, resultImageBytes.Length);

            resultBitMap.UnlockBits(imageData);

            return resultBitMap;
        }
    }
}