using System.Collections.Generic;
using System.Drawing;

namespace AnimationEngine
{
    public class SpriteToImagesConverter
    {
        private Image spriteImage;
        private readonly int _height;
        private readonly int _width;

        public SpriteToImagesConverter(Image spriteImage, int height, int width)
        {
            this.spriteImage = spriteImage;
            _height = height;
            _width = width;
        }

       
    }
}