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
        public bool Collide()
        {
            return true;
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
