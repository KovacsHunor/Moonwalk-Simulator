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
        public bool HorizontalCollide()
        {
            foreach (GameObject item in Global.Slices[Location.Y / Slice.Size][Location.X / Slice.Size].Objects)
            {
                if (item.Location.X < Location.X + Size.Width + Speed.X && item.Location.X > Location.X + Size.Width)
                {
                    Location.X += item.Location.X - (Location.X + Size.Width);
                    return true;
                }
                if (item.Location.X + item.Size.Width > Location.X + Speed.X && item.Location.X + item.Size.Width < Location.X)
                {
                    Location.X += Location.X - (item.Location.X + item.Size.Width);
                    return true;
                }
            }
            return false;
        }
        public bool VerticalCollide()
        {
            foreach (GameObject item in Global.Slices[Location.Y / Slice.Size][Location.X / Slice.Size].Objects)
            {
                if (item.Location.Y < Location.Y + Size.Height + Speed.Y && item.Location.Y > Location.Y + Size.Height)
                {
                    Location.Y += item.Location.Y - (Location.Y + Size.Height);
                    return true;
                }
                if (item.Location.Y + item.Size.Height > Location.Y + Speed.Y && item.Location.Y + item.Size.Height < Location.Y)
                {
                    Location.Y += Location.Y - (item.Location.Y + item.Size.Height);
                    return true;
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
        public void Move()
        {
            if (HorizontalCollide())
            {
                Speed.X = 0;
            }
            else
            {
                Location.X += Speed.X;
            }
            if (VerticalCollide())
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
            if (HorizontalCollide())
            {
                Speed.X *= -1;
            }
            else
            {
                Location.X += Speed.X;
            }
            if (VerticalCollide())
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
