using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Moonwalk_Simulator
{
    public class Slice
    {
        public static Image Sprite = Properties.Resources.slice;
        public List<GameObject> Objects = new List<GameObject>();
        public Point Position;
        public static int Size = 16*60;
    }
}
