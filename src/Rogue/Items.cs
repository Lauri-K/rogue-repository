using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace Rogue
{
    internal class Items
    {
        public string name;
        public Point2D position;
        public int spriteIndex;

        public Items(string name, Point2D position, int drawIndex)
        {
            this.name = name;
            this.position = position;
            this.spriteIndex = drawIndex;
        }
    }
}
