using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Resources;
namespace Moonwalk_Simulator
{
    public class GameObject
    {        
        public Image Sprite;
        public Point Location;
        public Size Size;
    }
    public class CollidingObject : GameObject
    {
        public Point Speed = new Point();
        public bool HorizontalCollide(int x, int y, bool recursion)
        {
            if (x >= 0 && y >= 0 && y < Global.Slices.Count && x < Global.Slices[y].Count)
            {
                foreach (GameObject item in Global.Slices[y][x].Objects)
                {
                    if (Location.Y < item.Location.Y + item.Size.Height + 1 && Location.Y + Size.Height > item.Location.Y - 1)
                    {
                        if (item.Location.X < Location.X + Size.Width + Speed.X + 1 && item.Location.X > Location.X + Size.Width)
                        {
                            Location.X = item.Location.X - Size.Width - 1;
                            return true;
                        }
                        if (item.Location.X + item.Size.Width > Location.X + Speed.X - 1 && item.Location.X + item.Size.Width < Location.X)
                        {
                            Location.X = item.Location.X + item.Size.Width + 1;
                            return true;
                        }
                    }
                }
            }
            if (this is Player && !recursion &&
                (Convert.ToInt32((Location.X + Speed.X + Math.Sign(Speed.X)) / (16 * 60)) != x ||
                 Convert.ToInt32((Location.Y + Speed.Y + Math.Sign(Speed.Y)) / (16 * 60)) != y))
            {
                return HorizontalCollide((Location.X + Speed.X) / (16 * 60), (Location.Y + Speed.Y) / (16 * 60), true);
            }
            return false;
        }
        public bool VerticalCollide(int x, int y, bool recursion)
        {
            
            if (x >= 0 && y >= 0 && y < Global.Slices.Count && x < Global.Slices[y].Count)
            {
                foreach (GameObject item in Global.Slices[y][x].Objects)
                {
                    if (Location.X < item.Location.X + item.Size.Width + 1 && Location.X + Size.Width > item.Location.X - 1)
                    {
                        if (item.Location.Y < Location.Y + Size.Height + Speed.Y + 1 && item.Location.Y > Location.Y + Size.Height)
                        {
                            if (this == Global.player)
                            {
                                Global.player.onGround = true;
                                Global.player.Jumping = false;
                            }
                            Location.Y = item.Location.Y - Size.Height - 1;
                            return true;
                        }
                        else if (this == Global.player)
                        {
                            Global.player.onGround = false;
                        }
                        if (item.Location.Y + item.Size.Height > Location.Y + Speed.Y - 1 && item.Location.Y + item.Size.Height < Location.Y)
                        {
                            Location.Y = item.Location.Y + item.Size.Height + 1;
                            return true;
                        }
                    }
                    else if (this == Global.player)
                    {
                        Global.player.onGround = false;
                    }
                }
            }
           
            if (this is Player && !recursion &&
                (Convert.ToInt32((Location.X + Speed.X + Math.Sign(Speed.X)) / (16 * 60)) != x ||
                 Convert.ToInt32((Location.Y + Speed.Y + Math.Sign(Speed.Y)) / (16 * 60)) != y))
            {
                return VerticalCollide((Location.X + Speed.X) / (16 * 60), (Location.Y + Speed.Y) / (16 * 60), true);
            }
            if (!Global.player.Jumping && this == Global.player && ((Global.player.onPlatform || Global.player.countPlatform >= 0) && !Global.player.onGround) && Global.player.fuel > 0)
            {
                return true;
            }
            return false;
        }
        public void Accelerate(Point Acceleration, int MaxSpeed)
        {
            Speed.X += Acceleration.X;
            Speed.Y += Acceleration.Y;
            if (Math.Abs(Speed.X) > MaxSpeed) 
            {
                Speed.X = Math.Sign(Speed.X) * MaxSpeed;
            }
        }
    }
    
    public class Player : CollidingObject
    {
        public Image[] moonwalkLeft;
        public Image[] moonwalkRight;

        public Player()
        {
            moonwalkLeft = new Image[20]
            { Properties.Resources._0, Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4,
              Properties.Resources._5, Properties.Resources._6, Properties.Resources._7, Properties.Resources._8, Properties.Resources._9,
              Properties.Resources._10, Properties.Resources._11, Properties.Resources._12, Properties.Resources._13, Properties.Resources._14,
              Properties.Resources._15, Properties.Resources._16, Properties.Resources._17, Properties.Resources._18, Properties.Resources._19};
            moonwalkRight = new Image[20]
            { Properties.Resources._0, Properties.Resources._1, Properties.Resources._2, Properties.Resources._3, Properties.Resources._4,
              Properties.Resources._5, Properties.Resources._6, Properties.Resources._7, Properties.Resources._8, Properties.Resources._9,
              Properties.Resources._10, Properties.Resources._11, Properties.Resources._12, Properties.Resources._13, Properties.Resources._14,
              Properties.Resources._15, Properties.Resources._16, Properties.Resources._17, Properties.Resources._18, Properties.Resources._19 };
            for (int i = 0; i < moonwalkRight.Length; i++)
            {
                moonwalkRight[i].RotateFlip(RotateFlipType.Rotate180FlipY);
            }
        }
        public bool Right;
        public bool Left;
        public bool onGround;
        public bool Jumping;
        public bool ShortJump;
        public int JumpLim;
        public bool onPlatform;
        public int countPlatform = -1;
        public int fuel;
        public bool platformJump = true;
        public void Move()
        {
            if (JumpLim == 1)
            {
                Speed.Y = -27;
                JumpLim++;
            }
            if (onPlatform)
            {
                fuel--;
            }
            if(onGround)
            {
                if (fuel < 100)
                {
                    fuel += 4;
                    platformJump = true;
                }
            }
            if(countPlatform > 0)
            {
                countPlatform--;
            }
            else if(countPlatform == 0)
            {
                onGround = false;
                countPlatform--;
            }
            if (ShortJump && Jumping && Speed.Y < 0)
            {
                Accelerate(new Point(0, 1), 200);
            }
            Accelerate(new Point(0, 1), 200); //gravity
            Point a = new Point(0, 0);
            if (Right)
            {
                a.X += 2;
            }
            else if (Speed.X > 0)
            {
                if (Speed.X - 4 < 0)
                {
                    Speed.X = 0;
                    a.X += 4;
                }
                a.X += -4;
            }
            if (Left)
            {
                a.X += -2;
            }
            else if (Speed.X < 0)
            {
                if (Speed.X + 4 > 0)
                {
                    Speed.X = 0;
                    a.X += -4;
                }
                a.X += 4;
            }
            if (Left && Right && onGround)
            {
                if (Math.Abs(Speed.X) > 0)
                {
                    if (Math.Abs(Speed.X) - 4 < 0)
                    {
                        Speed.X = 0;
                        a.X += Math.Sign(Speed.X)*4;
                    }
                    a.X += -1* Math.Sign(Speed.X) * 4;
                }
                else
                {
                    a.X = 0;
                }
            }
            Accelerate(a, 12);
            if (HorizontalCollide(Location.X / (16 * 60), Location.Y / (16 * 60), false))
            {
                Speed.X = 0;
            }
            else
            {
                Location.X += Speed.X;
            }
            if (VerticalCollide(Location.X / (16 * 60), Location.Y / (16 * 60), false))
            {
                Speed.Y = 0;
            }
            else
            {
                Location.Y += Speed.Y;
            }
            if (!onGround)
            {

            }
        }
    }
    public class Hat : CollidingObject
    {
        public bool Left = true;
        public bool Fly;
        public void Move()
        {
            if (HorizontalCollide(Location.X / (16 * 60), Location.Y / (16 * 60), false))
            {
                Speed.X *= -1;
            }
            else
            {
                Location.X += Speed.X;
            }
            if (VerticalCollide(Location.X / (16 * 60), Location.Y / (16 * 60), false))
            {
                Speed.Y *= -1;
            }
            else
            {
                Location.Y += Speed.Y;
            }
        }
    }
    public class Wall : GameObject
    {

    }
    public class Button : GameObject
    {

    }
    public class Door : GameObject
    {

    }
    public class Damage : GameObject
    {

    }
}
