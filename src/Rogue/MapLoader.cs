using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using ZeroElectric.Vinculum;
using TurboMapReader;

namespace Rogue
{
    internal class MapLoader
    {
        public Map loadTestMap()
        {
            Map test = new Map();
            test.mapWidth = 8;
            test.mapTiles = new int[] {
            2, 2, 2, 2, 2, 2, 2, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 2, 2,
            2, 2, 2, 2, 1, 1, 1, 2,
            2, 1, 1, 1, 1, 1, 1, 2,
            2, 2, 2, 2, 2, 2, 2, 2 };

            return test;
        }

       /* public void testFileReading(string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                Console.WriteLine("File contents:");
                Console.WriteLine();

                string line;
                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    Console.WriteLine(line);
                }

            }
        } */

        public Map loadMapFromFile(string filename)
        {
            bool fileFound = File.Exists(filename);
            if (fileFound == false)
            {
                Console.WriteLine($"File {filename} not found");
                return loadTestMap();
            }

            // Lataa tiedosto käyttäen TurboMapReaderia   
            TiledMap mapMadeInTiled = MapReader.LoadMapFromFile(filename);

            // Tarkista onnistuiko lataaminen
            if (mapMadeInTiled != null)
            {
                // Muuta Map olioksi ja palauta
                return ConvertTiledMapToMap(mapMadeInTiled);
            }
            else
            {
                // OH NO!
                return null;
            }

        }

        public Map ConvertTiledMapToMap(TiledMap turboMap)
        {
            // Luo tyhjä kenttä
            Map rogueMap = new Map();

            // Muunna tason "ground" tiedot
            TurboMapReader.MapLayer groundLayer = turboMap.GetLayerByName("ground");

            // TODO: Lue kentän leveys. Kaikilla TurboMapReader.MapLayer olioilla on sama leveys
            rogueMap.mapWidth = groundLayer.width;

            // Kuinka monta kenttäpalaa tässä tasossa on?
            int howManyTiles = groundLayer.data.Length;
            // Taulukko jossa palat ovat
            int[] groundTiles = groundLayer.data;

            // Luo uusi taso tietojen perusteella
            MapLayer myGroundLayer = new MapLayer(howManyTiles);
            myGroundLayer.name = "ground";


            //TODO: lue tason palat
            myGroundLayer.mapTiles = groundTiles;


            // Tallenna taso kenttään
            rogueMap.layers[0] = myGroundLayer;

            // TODO: Muunna tason "enemies" tiedot...
            TurboMapReader.MapLayer enemyLayer = turboMap.GetLayerByName("enemies");

            rogueMap.mapWidth = enemyLayer.width;

            int howManyEnemyTiles = enemyLayer.data.Length;
            int[] enemyTiles = enemyLayer.data; 

            MapLayer myEnemyLayer = new MapLayer(howManyEnemyTiles);
            myEnemyLayer.name = "enemies";

            myEnemyLayer.mapTiles = enemyTiles;

            rogueMap.layers[2] = myEnemyLayer;

            // TODO: Muunna tason "items" tiedot...
            TurboMapReader.MapLayer itemLayer = turboMap.GetLayerByName("items");

            rogueMap.mapWidth = itemLayer.width;

            int howManyItemTiles = itemLayer.data.Length;
            int[]itemTiles = itemLayer.data;

            MapLayer myItemLayer = new MapLayer(howManyItemTiles);
            myItemLayer.name = "items";

            myItemLayer.mapTiles = itemTiles;

            rogueMap.layers[1] = myItemLayer;

            // Lopulta palauta kenttä
            return rogueMap;
        }
    }
}
