using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ZeroElectric.Vinculum;
using static Rogue.Map;

namespace Rogue
{
    internal class Game
    {
        public static int tileSize = 16;
        static RenderTexture game_screen;
        static int gameWidth;
        static int gameHeight;
        static Image playerImage;
        public static Texture playerTexture;
        static string playerImageFilepath;
        public static Texture tileMapTexture;
        static Image enemyImage;
        public static Texture enemyTexture;
        static Image itemImage;
        public static Texture itemTexture;
        public string statusMessage = "";

        public void Run()
        {
            Console.CursorVisible = false;

            Console.WindowWidth = 60;
            Console.WindowHeight = 26;

            MapLoader loader = new MapLoader();
            Map level1 = loader.loadMapFromFile("Maps/tiledmap.tmj");

            PlayerCharacter player = new PlayerCharacter('@', Raylib.GREEN);
            player.position = new Point2D(2, 2);

            while (true)
            {
                Console.WriteLine("Name?");
                //player.name = Console.ReadLine();
                player.name = "testing";
                if (String.IsNullOrEmpty(player.name))
                {
                    Console.WriteLine("Name can't be empty.");
                    continue;
                }
                break;
            }
            while (true)
            {
                Console.WriteLine("Race?");
                Console.WriteLine("1: Human");
                Console.WriteLine("2: Elf");
                Console.WriteLine("3: Orc");
                //string raceAnswer = Console.ReadLine();
                string raceAnswer = "1";

                if (raceAnswer == "1") player.race = Race.Human;
                else if (raceAnswer == "2") player.race = Race.Elf;
                else if (raceAnswer == "3") player.race = Race.Orc;
                else
                {
                    Console.WriteLine("no");
                    continue;
                }
                break;

            }

            while (true)
            {
                Console.WriteLine("Class?");
                Console.WriteLine("1: Warrior");
                Console.WriteLine("2: Mage");
                Console.WriteLine("3: Rogue");
                //string ClassAnswer = Console.ReadLine();
                string ClassAnswer = "1";

                if (ClassAnswer == "1") player.plrClass = Class.Warrior;
                else if (ClassAnswer == "2") player.plrClass = Class.Mage;
                else if (ClassAnswer == "3") player.plrClass = Class.Rogue;
                else
                {
                    Console.WriteLine("no");
                    continue;
                }
                break;
            }

            Raylib.InitWindow(1000, 500, "Rogue");
           string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(currentDirectory);
            string playerImagePath = Path.Combine(currentDirectory, "Images", "PlayerImage.png");
            string itemImagePath = Path.Combine(currentDirectory, "Images", "ItemImage.png");
            string enemyImagePath = Path.Combine(currentDirectory, "Images", "EnemyImage.png");
            string tileMapImagePath = Path.Combine(currentDirectory, "Images", "tilemap_packed.png");
            playerImage = Raylib.LoadImage(playerImagePath);
            playerTexture = Raylib.LoadTexture(playerImagePath);
            enemyImage = Raylib.LoadImage(enemyImagePath);
            enemyTexture = Raylib.LoadTexture(enemyImagePath);
            itemImage = Raylib.LoadImage(itemImagePath);
            itemTexture = Raylib.LoadTexture(itemImagePath);
            itemImage = Raylib.LoadImage(itemImagePath);
            Image tileMapImage = Raylib.LoadImage(tileMapImagePath);
            tileMapTexture = Raylib.LoadTexture(tileMapImagePath);

            gameWidth = 480;  
            gameHeight = 270;
            game_screen = Raylib.LoadRenderTexture(gameWidth, gameHeight);
            Raylib.SetTextureFilter(game_screen.texture, TextureFilter.TEXTURE_FILTER_BILINEAR);

            bool gameRunning = true;
            int nextLine = 100;
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                level1.DrawEnemiesAndItems();
                level1.draw();
                player.Draw();
                Raylib.EndDrawing();

                int moveX = 0;
                int moveY = 0;
                  KeyboardKey key = Raylib.GetKeyPressedAsKeyboardKey();
                  switch (key)
                  {
                      case KeyboardKey.KEY_UP:
                          moveY = -1;
                          break;
                      case KeyboardKey.KEY_DOWN:
                          moveY = 1;
                          break;
                      case KeyboardKey.KEY_LEFT:
                          moveX = -1;
                          break;
                      case KeyboardKey.KEY_RIGHT:
                          moveX = 1;
                          break;

                      case KeyboardKey.KEY_ESCAPE:
                          gameRunning = false;
                          break;
                      default:
                          break;
                  };

                int NewX = player.position.x + moveX;
                int NewY = player.position.y + moveY;

                int index = NewX + NewY * level1.mapWidth;
                int value = level1.mapTiles[index];

                MapTile tile = level1.GetTileAt(NewX, NewY);
                if (tile != MapTile.Wall)
                {
                    player.Move(moveX, moveY);
                }

                if (player.vPos == level1.Enemytile)
                {
                    statusMessage = $"Hit enemy";
                    Raylib.DrawText(statusMessage, 10, 125, 20, Raylib.WHITE);
                }
                else if (player.vPos == level1.Itemtile)
                {
                    statusMessage = "Hit item";
                    Raylib.DrawText(statusMessage, 10, 150, 20, Raylib.WHITE);
                }
                statusMessage = "";
            }
        }
    }
}
