﻿using System;
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
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            GenerateMap("level0.txt");

        }

        void GenerateMap(string file)
        {
            StreamReader reader = new StreamReader(file);
            int y = 0;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                int x = 0;
                while (x < line.Length)
                {

                    if (line[x] == 'w')
                    {
                        Wall g = new Wall();
                        g.Location = new Point(x * 60, y * 60);
                        g.Size = new Size(60, 60);
                        g.Sprite = Image.FromFile(@"sprites\wall.png");

                        Global.GameObjects.Add(g);
                    }
                    else if (true)
                    {
                    }
                    x++;
                }
                y++;
            }
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
            if (!player.Collide())
            {
                player.Location.X += player.Speed.X;
                player.Location.Y += player.Speed.Y;
            }
            Refresh();
        }
    }
}
