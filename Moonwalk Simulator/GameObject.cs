using System.Drawing;

namespace Moonwalk_Simulator
{
    public class GameObject
    {
        public Point Speed = new Point();
        public Image Sprite;
        public Point Location;
        public Size Size;
        public bool HorizontalCollide()
        {
            foreach (GameObject item in Global.Slices[Location.Y / Slice.Size][Location.X / Slice.Size].Objects)
            {
                if (item.Location.X < Location.X + Size.Width + Speed.X && item.Location.X > Location.X + Size.Width)
                {
                    return true;
                }
                if (item.Location.X + item.Size.Width > Location.X + Speed.X && item.Location.X + item.Size.Width < Location.X)
                {
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
                    if(this == Global.player)
                    {
                        Global.player.onGround = true;
                    }
                    return true;
                }
                if (item.Location.Y + item.Size.Height > Location.Y + Speed.Y && item.Location.Y + item.Size.Height < Location.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class Player : GameObject
    {
        public bool Right;
        public bool Left;
        public bool onGround;
        public void Move()
        {
          
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
