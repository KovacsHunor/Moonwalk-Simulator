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
        Point posConst = new Point(1920/2, 2*1080/3);

        public Form1()
        {
            
            Global.GameObjects.Add(Global.player);
            player.Sprite = Properties.Resources.wall;
            player.Location = new Point(0,0);
            player.Size = new Size(30,60);
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
                    while (Global.Slices.Count < x/16 + 1)
                    {
                        Global.Slices.Add(new List<Slice>());
                    }
                    for (int j = 0; j < Global.Slices.Count; j++)
                    {
                        while (Global.Slices[j].Count < y/16 + 1)
                        {
                            Global.Slices[j].Add(new Slice());
                        }
                    }
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
            Global.GameObjects.Add(g);
            Global.Slices[x / 16][y / 16].Objects.Add(g);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(Screen.PrimaryScreen.Bounds.Width / 1920f, Screen.PrimaryScreen.Bounds.Height / 1080f);
            foreach (GameObject obj in Global.GameObjects)
            {
                int d = 0;
                if (obj is Wall)
                {
                    d++;
                }
                e.Graphics.DrawImage(obj.Sprite,
                                     obj.Location.X - d + posConst.X - player.Location.X,
                                     obj.Location.Y - d + posConst.Y - player.Location.Y,
                                     obj.Size.Width + 2 * d, obj.Size.Height + 2 * d);
            }
        }

        private void main_Tick(object sender, EventArgs e)
        {
            player.Move();
            Refresh();
        }
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                player.Left = true;
            }
            if (e.KeyCode == Keys.D)
            {
                player.Right = true;
            }
            if (e.KeyCode == Keys.Space && player.onGround)
            {
                player.ShortJump = false;
                player.Jumping = true;
                player.JumpLim++;
            }
            if (e.KeyCode == Keys.B)
            {
                //debug
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                player.Left = false;
            }
            if (e.KeyCode == Keys.D)
            {
                player.Right = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                player.ShortJump = true;
                player.JumpLim = 0;
            }
        }
    }
}
