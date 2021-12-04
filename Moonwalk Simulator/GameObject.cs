using System;
using System.Drawing;

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
        public bool HorizontalCollide(int x, int y)
        {
            foreach (GameObject item in Global.Slices[y/16/60][x/16/60].Objects)
            {
                if (Location.Y < item.Location.Y + item.Size.Height && Location.Y + Size.Height > item.Location.Y)
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
            return false;
        }
        public bool VerticalCollide(int x, int y)
        {
            foreach (GameObject item in Global.Slices[y/16/60][x/16/60].Objects)
            {
                if (Location.X < item.Location.X + item.Size.Width && Location.X + Size.Width > item.Location.X)
                {
                    if (item.Location.Y < Location.Y + Size.Height + Speed.Y + 1 && item.Location.Y > Location.Y + Size.Height)
                    {
                        if (this == Global.player)
                        {
                            Global.player.onGround = true;
                            
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
        public bool Right;
        public bool Left;
        public bool onGround;
        public void Move()
        {
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
            Accelerate(a, 15);
            if (HorizontalCollide(Global.player.Location.X,Global.player.Location.Y))
            {
                Speed.X = 0;
            }
            else
            {
                Location.X += Speed.X;
            }
            if (VerticalCollide(Global.player.Location.X,Global.player.Location.Y))
            {
                Speed.Y = 0;
            }
            else
            {
                Location.Y += Speed.Y;
            }
        }
    }
    public class Hat : CollidingObject
    {
        public void Move()
        {
            if (HorizontalCollide(0,0))
            {
                Speed.X *= -1;
            }
            else
            {
                Location.X += Speed.X;
            }
            if (VerticalCollide(0,0))
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
