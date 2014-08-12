using System.Drawing;
using System.Windows.Forms;
using AnimationEngine;


namespace SpritesPlayer
{
    public partial class Form1 : Form
    {
        private MultipleImagesAnimation Animation { get; set; }

        public Form1()
        {
            InitializeComponent();

            var newImage =
                Image.FromFile(@"C:\Users\Микола\Documents\GitHub\Cossacks-on-Canvas\images_png\SWRG\output\4.png");

            Animation = new MultipleImagesAnimation();

            Animation.Target(pictureEdit1, newImage, 20, 112, 16);

            Animation.Play();
        }

        private void pictureEdit1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Animation != null)
            {
                var pen = new Pen(Color.Black);

                e.Graphics.DrawLine(pen, 0, Animation.Image.Height + Animation.yOffset, 100,
                    Animation.Image.Height + Animation.yOffset);

                e.Graphics.DrawImage(Animation.Image, new Point(50, Animation.yOffset));
                e.Graphics.DrawImage(Animation.Image, new Point(50, Animation.Image.Height + Animation.yOffset));
            }
        }
    }
}
