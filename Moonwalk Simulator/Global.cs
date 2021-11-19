using System;
using System.Collections.Generic;
using System.Text;

namespace Moonwalk_Simulator
{
    public class Global
    {
        List<List<Slice>> Slices = new List<List<Slice>>();
        public static List<GameObject> GameObjects = new List<GameObject>();
        public static Player player = new Player();
    }
}
