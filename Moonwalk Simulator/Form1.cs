using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Resources;

namespace Moonwalk_Simulator
{
    public partial class Form1 : Form
    {
        static Player player = Global.player;
        static Hat hat = Global.hat;
       
        
        GameObject fuel0 = new GameObject();
        GameObject fuel1 = new GameObject();
        GameObject platform = new GameObject();
        public Form1()
        {
            Global.GameObjects.Add(Global.player);
            Global.GameObjects.Add(Global.hat);

            hat.Sprite = Properties.Resources.hatr;
            hat.Location = new Point(0, 0);
            hat.Size = new Size(28, 16);

            player.Sprite = Properties.Resources.jackson;
            player.Location = new Point(0,0);
            player.Size = new Size(30,60);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            //Menu();
            GenerateMap(Properties.Resources.level0);
            hat.Const = new Point(Global.posConst.X + 14, Global.posConst.Y + 8);
            hat.Pos = new Point(Global.posConst.X + 14, Global.posConst.Y + 8);
            fuel0.Size = new Size(202, 17);
            fuel0.Sprite = Properties.Resources.fuel0;
            Global.GameObjects.Add(fuel0);

            fuel1.Size = new Size(200, 15);
            fuel1.Sprite = Properties.Resources.fuel1;
            Global.GameObjects.Add(fuel1);

            platform.Sprite = Properties.Resources.empty;
            platform.Size = new Size(34, 1);
            Global.GameObjects.Add(platform);

            start.Sprite = Properties.Resources.button;
            start.Size = new Size(60, 60);
        }
        bool menu = false;
        void Menu()
        {
            menu = true;
            main.Enabled = false;
            moonwalk.Enabled = false;
            Global.MenuObjects.Add(start);
          //  Global.MenuObjects.Add(exit);
           // Global.MenuObjects.Add(options);
        }
        void GenerateMap(string file)
        {
            string[] lines = file.Split('\n');
            int y = 0;
            foreach (string line in lines )
            {
                int z = 0;
                int x = 0;
                while (x < line.Length)
                {
                    while (Global.Slices.Count < y/16 + 1)
                    {
                        Global.Slices.Add(new List<Slice>());
                    }
                    for (int j = 0; j < Global.Slices.Count; j++)
                    {
                        while (Global.Slices[j].Count < x-z/16 + 1)
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
                        g.Sprite = Properties.Resources.button;
                        GenerateObject(g, x, y);
                    }
                    else if (line[x] == 'd')
                    {
                        Door g = new Door();
                        Global.Doors.Add(g);
                        g.Sprite = Properties.Resources.door;
                        GenerateObject(g, x, y);
                    }
                    else if (line[x] == 'x')
                    {
                        Damage g = new Damage();
                        g.Sprite = Properties.Resources.danger;
                        GenerateObject(g, x, y);

                    }
                    else if (line[x] == 's')
                    {
                        Spawn g = new Spawn();
                        g.Sprite = Properties.Resources.spawn;
                        GenerateObject(g, x, y);

                    }
                    x++;
                }
                y++;
            }
        }
        void GenerateObject(Wall g, int x, int y)
        {
            g.Location = new Point(x * 60, y * 60);
            g.Size = new Size(60, 60);
            Global.GameObjects.Add(g);
            if (g is Door)
            {

            }
            else
            {
                Global.Slices[y / 16][x / 16].Objects.Add(g);
            }
            g.SlicePosy = y / 16;
            g.SlicePosx = x / 16;
        }
        bool ClickObject (GameObject a)
        {
            if (Form1.MousePosition.X > a.Location.X && Form1.MousePosition.X < a.Location.X + a.Size.Width && Form1.MousePosition.Y > a.Location.Y && Form1.MousePosition.Y < a.Location.Y + a.Size.Height)
            {
                return true;
            }
            return false;
        }
        GameObject start = new GameObject();
        
      //  GameObject exit = new GameObject();
      //GameObject options = new GameObject();
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(Screen.PrimaryScreen.Bounds.Width / 1920f, Screen.PrimaryScreen.Bounds.Height / 1080f);
            if (menu)
            {
                foreach (GameObject obj in Global.MenuObjects)
                {
                    e.Graphics.DrawImage(obj.Sprite, obj.Location);
                }
            }
            else
            {
                foreach (GameObject obj in Global.GameObjects)
                {
                    int d = 0;
                    if (obj is Wall || obj is Damage)
                    {
                        d++;
                    }
                    e.Graphics.DrawImage(obj.Sprite,
                                         obj.Location.X - d + Global.posConst.X - player.Location.X,
                                         obj.Location.Y - d + Global.posConst.Y - player.Location.Y,
                                         obj.Size.Width + 2 * d, obj.Size.Height + 2 * d);
                }
            }
           
        }

        private void main_Tick(object sender, EventArgs e)
        {

            player.Move();
            hat.Move();
            hat.Speed.X = (int)(Math.Sqrt(100 / (float)(Math.Pow(hat.Aim.X, 2) + Math.Pow(hat.Aim.Y, 2))) * hat.Aim.X);
            hat.Speed.Y = (int)(Math.Sqrt(100 / (float)(Math.Pow(hat.Aim.X, 2) + Math.Pow(hat.Aim.Y, 2))) * hat.Aim.Y);

            hat.Aim.X -= hat.Speed.X;
            hat.Aim.Y -= hat.Speed.Y;
            if (hat.Fly && (Math.Abs(hat.Aim.X) < 10 || Math.Abs(hat.Aim.Y) < 10))
            {
                hat.Aim.X += hat.Speed.X;
                hat.Aim.Y += hat.Speed.Y;
            }
            if (player.fallCounter % 50 == 0)
            {
                player.health--;
                
            }
            if (player.Place is Damage)
            {
                if (player.damageCount == 0)
                {
                    player.health--;
                    player.damageCount = 50;
                }
                else
                {
                    player.damageCount--;
                }
            }
            else if(player.Place is Spawn)
            {
                player.spawn.X = player.Place.Location.X;
                player.spawn.Y = player.Place.Location.Y - 200;
            }
            if (spacepress && !spacedown)
            {
                if (((player.countPlatform > 0 && player.platformJump) || player.onGround) && !player.onPlatform && player.fuel > 0)
                {
                    if (player.countPlatform > 0)
                    {
                        player.platformJump = false;
                    }
                    player.ShortJump = false;
                    player.Jumping = true;
                    if (player.JumpLim == 0 && player.countPlatform != 0)
                    {
                        player.JumpLim++;
                    }
                }
            }
            fuel1.Size.Width = player.fuel * 2;
            if(fuel1.Size.Width < 0)
            {
                fuel1.Size.Width = 0;
            }
            else if (fuel1.Size.Width > 200)
            {
                fuel1.Size.Width = 200;
            }
            fuel0.Location = player.Location;
            fuel0.Location.X += 700;
            fuel0.Location.Y -= 600;
            fuel1.Location = player.Location;
            fuel1.Location.X += 701;
            fuel1.Location.Y -= 599;
            platform.Location.X = player.Location.X - 2;
            platform.Location.Y = player.Location.Y + 61;
            if (!player.onGround && (player.countPlatform > 0 || (player.onPlatform && player.fuel > 0)))
            {
                platform.Sprite = Properties.Resources.platform;
            }
            else
            {
                platform.Sprite = Properties.Resources.empty;
            }
            if (!hat.Fly)
            {
                if (hat.Left)
                {
                    hat.Sprite = Properties.Resources.hat;
                    hat.Location.X = player.Location.X + 1;
                    
                }
                else
                {
                    hat.Sprite = Properties.Resources.hatr;
                    hat.Location.X = player.Location.X + 1;
                }
                hat.Location.Y = player.Location.Y;
            }
            if (player.health == 0)
            {
                player.health = 3;
                player.Location = player.spawn;
                player.fuel = 100;
                player.onPlatform = false;
                player.onGround = false;
            }
        }
        public Point hatConst = new Point(Global.posConst.X + 14, Global.posConst.Y + 8);
        void Resume()
        {
            menu = false;
            moonwalk.Enabled = true;
            main.Enabled = true;
            Global.MenuObjects.Remove(start);
        }
        public void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right && !player.onPlatform && !player.onGround && player.fuel > 0)
            {
                player.onPlatform = true;
                player.Jumping = false;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (menu && ClickObject(start))
                {
                    Resume();
                }
                else if (!hat.Fly && !menu)
                {
                    hat.Fly = true;
                    hat.Aim.X = (Cursor.Position.X - hat.Const.X);
                    hat.Aim.Y = (Cursor.Position.Y - hat.Const.Y);
                 //  hat.Speed.X = (int)(Math.Sqrt(100 / (float)(Math.Pow(hat.Aim.X, 2) + Math.Pow(hat.Aim.Y, 2))) * hat.Aim.X);
                   //hat.Speed.Y = (int)(Math.Sqrt(100 / (float)(Math.Pow(hat.Aim.X, 2) + Math.Pow(hat.Aim.Y, 2))) * hat.Aim.Y);

                }
            }
        }
        public void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && player.onPlatform)
            {
                player.onPlatform = false;
                if (player.fuel > 0)
                {
                    player.countPlatform = 20;
                }
            }
        }
        bool spacedown;
        bool spacepress;
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                player.Left = true;
                hat.Left = true;
            }
            if (e.KeyCode == Keys.D)
            {
                player.Right = true;
                hat.Left = false;
            }
            if (e.KeyCode == Keys.Space && !spacedown)
            {
                spacepress = true;
                if (player.onPlatform && !player.onGround)
                {
                    spacedown = true;
                }
            }
            if (e.KeyCode == Keys.Q)
            {
                hat.Fly = false;
            }
            if (e.KeyCode == Keys.Escape)
            {
                
                if (menu)
                {
                    Resume();
                }
                else
                {
                    Menu();
                }
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
                if (!player.Right)
                {
                    player.Sprite = player.moonwalkLeft[0];
                }
            }
            if (e.KeyCode == Keys.D)
            {
                player.Right = false;
                if (!player.Left)
                {
                    player.Sprite = player.moonwalkRight[0];
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                player.ShortJump = true;
                player.JumpLim = 0;
                spacedown = false;
                spacepress = false;
            }
        }

        public static int anim = 0;
        bool spin;
        private void moowalk_Tick(object sender, EventArgs e)
        {
            if (player.Left)
            {
                player.Sprite = player.moonwalkLeft[anim];
            }
            else if (player.Right)
            {
                player.Sprite = player.moonwalkRight[anim];
            }
            if (player.countPlatform > 0 || player.onPlatform || player.onGround)
            {
                anim = (anim + 1) % 20;
            }
            if (player.Left && player.Right)
            {
                anim = 0;
                player.Sprite = player.moonwalkRight[anim];
                if (spin)
                {
                    player.Sprite = player.moonwalkLeft[anim];
                }
                spin = !spin;
            }
        }

        private void refresh_Tick(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
