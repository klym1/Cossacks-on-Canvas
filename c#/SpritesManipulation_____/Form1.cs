using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AnimationEngine;

namespace SpritesManipulation
{
    public partial class Form1 : Form
    {
        private static readonly List<Bitmap> All = new List<Bitmap>();
        private Animation animation;

        const string baseDirectory = @"..\..\..\..\images_png\";

        public Form1()
        {
            InitializeComponent();
            
            animation = new Animation();
            
            foreach (var dir in Directory.GetParent(baseDirectory).EnumerateDirectories())
            {
                Debug.WriteLine(String.Format("Checked: {0}", dir.Name));
                
                comboBoxEdit1.Properties.Items.Add(dir.FullName);
            }
            
            animation.Target(pictureEditAlpha);

           spinEdit1.EditValueChanged += (sender, args) => GetValue(animation, (int)(decimal)spinEdit1.EditValue, imagesNumber);

            comboBoxEdit1.EditValueChanged += (sender, args) =>
            {
                imagesNumber = GetImagesNumber((string)comboBoxEdit1.EditValue);
                GetValue(animation, 0, imagesNumber);
            };
        }

        private static int imagesNumber;
        
        private static int GetImagesNumber(string basePath)
        {
            var imagesNumber = 0;

            All.Clear();
            
                foreach (var file in Directory.EnumerateFiles(basePath, "*.png"))
                {
                    All.Add(new Bitmap(Image.FromFile(file)));
                    imagesNumber++;
                }
            
            return imagesNumber;
        }

        private static void GetValue(Animation animation, int pos, int imagesNumber )
        {
            animation.Stop();


            animation.Frames.Clear();
            
            for (int i = pos; i <imagesNumber; i += 9)
            {
                  animation.AddFrame(All[i]);
            }
            
            animation.Play();
        }
        
        private void pictureEditAlpha_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle bounds = new Rectangle
            {
                X = 100, 
                Y = 100, 
                Size = animation.Image.Size
            };

            ControlPaint.DrawBorder(e.Graphics, bounds,
                                         Color.Black, 1, ButtonBorderStyle.Inset,
                                         Color.Black, 1, ButtonBorderStyle.Inset,
                                         Color.Black, 1, ButtonBorderStyle.Inset,
                                         Color.Black, 1, ButtonBorderStyle.Inset);

                e.Graphics.DrawImage(animation.Image, new Point(50, 100));
           
        }
        
    }
}
