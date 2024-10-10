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
        public Vector2 position;
        public Texture graphics;
        public int drawIndex;

        public Items(string name, Vector2 position, Texture graphics, int drawIndex)
        {
            this.name = name;
            this.position = position;
            this.graphics = graphics;
            this.drawIndex = drawIndex;
        }
    }
}
