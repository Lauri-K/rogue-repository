using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TurboMapReader;
using ZeroElectric.Vinculum;

namespace Rogue
{
    internal class Map
    {

        public int mapWidth = 8;
        public int[] mapTiles;
        public MapLayer[] layers;
        public List<Enemy> enemies;
        public List<Items> items;
        public Vector2 Enemytile;
        public Vector2 Itemtile;
        public enum MapTile : int
        {
            Floor = 48,
            Wall = 40
        }

        public static List<int> WallTileNumbers;
        public static List<int> FloorTileNumbers;

        public Map()
        {
            mapWidth = 1;
            layers = new MapLayer[3];
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new MapLayer(mapWidth);
            }
            enemies = new List<Enemy>() { };
            items = new List<Items>() { };
        }
        public MapLayer GetLayer(string layerName)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (layers[i].name == layerName)
                {
                    return layers[i];
                }
            }
            Console.WriteLine($"Error: No layer with name: {layerName}");
            return null;
        }

        public MapTile GetTileAt(int x, int y)
        {
            WallTileNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 25, 26, 27, 28, 29, 40, 57, 58, 59 };
            FloorTileNumbers = new List<int> { 49 };
            // Calculate index: index = x + y * mapWidth
            int indexInMap = x + y * mapWidth;

            // Use the index to get a map tile from map's array
            MapLayer groundLayer = GetLayer("ground");
            int[] mapTiles = groundLayer.mapTiles;
            int tileId = mapTiles[indexInMap];

            if (WallTileNumbers.Contains(tileId))
            {
                // Is a wall
                return MapTile.Wall;
            }
            else if (FloorTileNumbers.Contains(tileId))
            {
                // One of the floortiles
                return MapTile.Floor;
            }
            else
            {
                // Count everything else as wall for now.
                return MapTile.Wall;
            }
        }
        public Vector2 GetSpritePosition(int spriteIndex, int spritesPerRow)
        {
            float spritePixelX = (spriteIndex % spritesPerRow) * Game.tileSize;
            float spritePixelY = (int)(spriteIndex / spritesPerRow) * Game.tileSize;
            return new Vector2(spritePixelX, spritePixelY);
        }

        public void draw()
        {
            MapLayer groundLayer = GetLayer("ground");
            mapTiles = groundLayer.mapTiles;
            int mapHeight = mapTiles.Length / mapWidth;
            for (int row = 0; row < mapHeight; row++)
            {
                for (int col = 0; col < mapWidth; col++)
                {
                    int index = col + row * mapWidth;
                    int tileId = mapTiles[index];
                    int spriteId = tileId -1;
                    int pixelX = col * Game.tileSize;
                    int pixelY = row * Game.tileSize;
                    //int imagePixelX = tileIndex % Game.imagesPerRow * Game.tileSize;
                    //int imagePixelY = tileIndex / Game.imagesPerRow * Game.tileSize;
                    //Rectangle mapRect = new Rectangle(imagePixelX, imagePixelY, Game.tileSize, Game.tileSize);

                    Rectangle rectangle = new Rectangle(pixelY, pixelX, Game.tileSize, Game.tileSize);
                    //Raylib.DrawTextureRec(Game.tileMapTexture, rectangle, GetSpritePosition(tileId, mapWidth), Raylib.WHITE);
                    Raylib.DrawTextureRec(Game.tileMapTexture, rectangle, new Vector2(col, row)* Game.tileSize, Raylib.WHITE);

                    /*Console.SetCursorPosition(col, row);
                    Color tilecolor = Raylib.GREEN;
                    switch (tileId)
                    {
                        case 1:
                            tilecolor = Raylib.GRAY;
                            Console.Write(".");
                            break;
                        case 2:
                            tilecolor = Raylib.BROWN;
                            Console.Write("#");
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }
                    Raylib.DrawRectangle(pixelX, pixelY, Game.tileSize, Game.tileSize, tilecolor);*/
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                Items currentItems = items[i];
                Vector2 itemPosition = currentItems.position;
                int itemSpriteindex = currentItems.drawIndex;
                Raylib.DrawTextureV(Game.itemTexture, itemPosition, Raylib.WHITE);
                Itemtile = itemPosition;
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy currentEnemy = enemies[i];
                Vector2 enemyPosition = currentEnemy.Position;
                int enemySpriteIndex = currentEnemy.drawIndex;
                Raylib.DrawTextureV(Game.enemyTexture, enemyPosition, Raylib.WHITE);
                Enemytile = enemyPosition;
            }
        }
        public void DrawEnemiesAndItems()
        {
            enemies = new List<Enemy>();


            MapLayer enemyLayer = GetLayer("enemies");

            int[] enemyTiles = enemyLayer.mapTiles;
            int mapHeight = enemyTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Vector2 position = new Vector2(x * Game.tileSize, y * Game.tileSize);

                    int index = x + y * mapWidth;
                    int tileId = enemyTiles[index];
                    switch (tileId)
                    {
                        case 0:
                            break;
                        case 1:
                            enemies.Add(new Enemy("Orc", position, Game.enemyTexture, tileId));
                            break;
                        case 2:
                            break;
                    }
                }
            }

            items = new List<Items>();


            MapLayer itemLayer = GetLayer("items");

            int[] itemTiles = itemLayer.mapTiles;
            mapHeight = itemTiles.Length / mapWidth;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    Vector2 position = new Vector2 (x * Game.tileSize, y * Game.tileSize);

                    int index = x + y * mapWidth;
                    int tileId = itemTiles[index];
                    switch (tileId)
                    {
                        case 0:
                            break; 
                        case 1:
                            items.Add(new Items("name", position, Game.itemTexture, tileId));
                            break; 
                        case 2:
                            break;
                    }
                }
            }
        }

    }
}
