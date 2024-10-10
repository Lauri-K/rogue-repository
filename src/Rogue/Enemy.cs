using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace Rogue
{
    internal class Enemy
    {
        public string Name;
        public Vector2 Position;
        public Texture graphics;
        public int drawIndex;
        public Enemy(string name, Vector2 position, Texture graphics, int drawIndex)
        {
            this.Name = name;
            this.Position = position;
            this.graphics = graphics;
            this.drawIndex = drawIndex;
        }
    }
}
