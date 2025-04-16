using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace Rogue
{
    public struct Enemy
    {
        public string Name;
        public Point2D Position;
        public int Hp;
        public int SpriteIndex;
        public Enemy(string name, Point2D position, int hp, int spriteIndex)
        {
            this.Name = name;
            this.Position = position;
            this.Hp = hp;
            this.SpriteIndex = spriteIndex;
        }
    }
}
