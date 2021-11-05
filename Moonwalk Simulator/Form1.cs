using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Moonwalk_Simulator
{
    public partial class Form1 : Form
    {
        Point posConst;       
        float scale = Screen.PrimaryScreen.Bounds.Height / 1080f;

        public Form1()
        {
            Global.GameObjects.Add(Global.player);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (GameObject obj in Global.GameObjects)
            {
                e.Graphics.DrawImage(obj.Sprite,
                                     obj.Location.X * scale + posConst.X - Global.player.Location.X, 
                                     obj.Location.Y * scale + posConst.Y - Global.player.Location.Y, 
                                     obj.Size.Width * scale, obj.Size.Height * scale);
            }
        }
    }
}
