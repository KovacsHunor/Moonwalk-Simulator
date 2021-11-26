using System.Drawing;

namespace Moonwalk_Simulator
{
    public class GameObject
    {        
        public Image Sprite;
        public Point Location;
        public Size Size;
    }
    public class Player : GameObject
    {
        public Point Speed = new Point();
        public bool HorizontalCollide()
        {
            foreach (GameObject item in Global.Slices[Location.Y / Slice.Size][Location.X / Slice.Size].Objects)
            {
                if (item.Location.X < Location.X + Size.Width + Speed.X && item.Location.X > Location.X)
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
                if (item.Location.Y < Location.Y + Size.Height + Speed.Y && item.Location.Y > Location.Y)
                {
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
