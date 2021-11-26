using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Moonwalk_Simulator
{
    public partial class Form1 : Form
    {        
        static Player player = Global.player;
        Point posConst;       
        float scale = Screen.PrimaryScreen.Bounds.Height / 1080f;

        public Form1()
        {
            Global.GameObjects.Add(Global.player);
            player.Sprite = Properties.Resources.wall;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            GenerateMap(Properties.Resources.level0);
        }

        void GenerateMap(string file)
        {
            string[] lines = file.Split('\n');
            int y = 0;
            foreach (string line in lines )
            { 
                int x = 0;
                while (x < line.Length)
                {
                    if (line[x] == 'w')
                    {
                        Wall g = new Wall();
                        g.Sprite = Properties.Resources.wall;
                        GenerateObject(g, x, y);
                    }
                    else if (line[x] == 'b')
                    {
                        Button g = new Button();
                        g.Sprite = Image.FromFile(@"sprites\button.png");
                        GenerateObject(g, x, y);
                    }
                    else if (line[x] == 'd')
                    {
                        Door g = new Door();
                        g.Sprite = Image.FromFile(@"sprites\door.png");
                        GenerateObject(g, x, y);
                    }
                    else if (line[x] == 'x')
                    {
                        Damage g = new Damage();
                        g.Sprite = Image.FromFile(@"sprites\damage.png");
                        GenerateObject(g, x, y);
                    }
                    x++;
                }
                y++;
            }
        }
        void GenerateObject(GameObject g, int x, int y)
        {
            g.Location = new Point(x * 60, y * 60);
            g.Size = new Size(60, 60);
            if (Global.Slices.Count < (y / 16) + 1)
            {
                Global.Slices.Add(new List<Slice>());
            }
            if (Global.Slices[y / 16].Count < (x / 16) + 1)
            {
                Global.Slices[y / 16].Add(new Slice());
            }
            Global.GameObjects.Add(g);
            Global.Slices[y / 16][x / 16].Objects.Add(g);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (GameObject obj in Global.GameObjects)
            {
                e.Graphics.DrawImage(obj.Sprite,
                                     obj.Location.X * scale + posConst.X - player.Location.X,
                                     obj.Location.Y * scale + posConst.Y - player.Location.Y,
                                     obj.Size.Width * scale, obj.Size.Height * scale);
            }
        }

        private void main_Tick(object sender, EventArgs e)
        {
            if (!player.HorizontalCollide())
            {
                player.Location.X += player.Speed.X;
            }
            if (!player.VerticalCollide())
            {
                player.Location.Y += player.Speed.Y;
            }
            Refresh();
        }
    }
}
