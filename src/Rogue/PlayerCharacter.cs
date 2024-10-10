using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;

namespace Rogue
{

    public enum Race
    {
        Human,
        Elf,
        Orc
    }

    public enum Class
    {
        Warrior,
        Mage,
        Rogue
    }

    internal class PlayerCharacter
    {
        public string name;
        public Race race;
        public Class plrClass;

        public Point2D position;
        public Vector2 vPos;

        private char image;
        private Color color;

        Texture playerImage;
        int imagePixelX;
        int imagePixelY;

        public PlayerCharacter (char image, Color color)
        {
            this.image = image;
            this.color = color;
        }
        public void Move(int x_move, int y_move)
        {
            position.x += x_move;
            position.y += y_move;

            position.x = Math.Clamp(position.x, 0, Console.WindowWidth - 1);
            position.y = Math.Clamp(position.y, 0, Console.WindowHeight - 1);


        }
        public void Draw()
        {
            int pixelX = position.x * Game.tileSize;
            int pixelY = position.y * Game.tileSize;
            vPos = new Vector2(pixelX, pixelY);
            Console.SetCursorPosition(position.x, position.y);
            Raylib.DrawTextureV(Game.playerTexture, vPos, Raylib.WHITE);
        }

    }
}
