using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;

namespace Moonwalk_Simulator
{
    public class Global
    {
        public static List<List<Slice>> Slices = new List<List<Slice>>();
       
        public static List<GameObject> GameObjects = new List<GameObject>();
        public static List<GameObject> MenuObjects = new List<GameObject>();
        public static List<Door> Doors = new List<Door>();
        public static Player player = new Player();
        public static Hat hat = new Hat();
        public static Point posConst = new Point((1920 - 30) / 2, 2 * 1080 / 3);
    }
}
