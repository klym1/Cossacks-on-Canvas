using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AnimationEngine
{
    public class MultipleImagesAnimation
    {
        private readonly Timer _animtimer = new Timer();
        
        public int FrameIndex;

        private PictureEdit _ptarget;

        public void Target(PictureEdit target, Image image, int spriteWidth, int spriteHeight, int count)
        {
            _ptarget = target;

            Image = image;

            this.SpriteWidth = spriteWidth;
            this.SpriteHeight = spriteHeight;

            xOffset = 0; 
            yOffset = 0;

            Count = count;
            TempCount = 0;
        }

        public int Count { get; set; }
        public int TempCount { get; set; }

        public int FrameSpeed
        {
            get { return _animtimer.Interval; }
            set { _animtimer.Interval = value; }
        }

        public MultipleImagesAnimation()
        {
            _animtimer.Interval = 33;
            _animtimer.Tick += Update;
        }

        public void Play()
        {
            _animtimer.Start();
        }
         
        private void Update(object sender, EventArgs eventArgs)
        {
            yOffset -= SpriteHeight;

            TempCount++;
            
            _ptarget.Invalidate();

            if (TempCount > Count)
            {
                TempCount = 0;
                yOffset = 0;
            }
        }

        public Image Image { get; set; }

        public int offset { get; set; }

        public int xOffset { get; set; }

        public int yOffset { get; set; }

        public int SpriteWidth { get; set; }

        public int SpriteHeight { get; set; }
    }
}
