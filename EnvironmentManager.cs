using System;
using Microsoft.Xna.Framework;


namespace DalePetJump {

    public class EnvironmentManager {

        int [,] _map;

        // target platfroms
        int totalPlatforms = 25; 

        // a margin to make it more like doodle jump
        int safetyMarginX = 5; 
        int safetyMarginY = 5; 

        private Vector2 _spawnPoint;

        public EnvironmentManager(int [,] map) {
            _map = map;
        }

        public void SpawnPlatforms() {

            Random random = new Random();

            for(int platforms = 0; platforms <=  totalPlatforms; platforms++) {
                int randomWorldX =  random.Next(0,  Game1.worldWidth);
                int randomWorldY =  random.Next(0 + safetyMarginY,  Game1.worldHeight - safetyMarginY);

                Console.WriteLine("Platform Position Test " + platforms);

                // quick and crappy way to make sure we don't spawn dupes 

                if(GetTile(randomWorldX, randomWorldY) == 0 && GetTile(randomWorldX + 1, randomWorldY) == 0) {

                    _map[randomWorldY,randomWorldX] = 1;
                    _map[randomWorldY,randomWorldX + 1] = 1;

                    _spawnPoint = new Vector2(randomWorldX * Game1.tileSizeWidth , randomWorldY * Game1.tileSizeHeight - 32);

                    Console.WriteLine(_spawnPoint);

                }  
            } 
        }

        public void ClearPlatforms() {
            Array.Clear(_map, 0, _map.Length);
        }
        
        public void Update() {

            if(Game1.player._position.Y > Game1.gameResHeight) {
                Game1.player._position.Y = GetSpawnPoint().Y;
            }

        }

        public int GetTile(int x, int y) {
            // this expects tile as * 16
            // not the raw
            if(0 <= x && x < Game1.worldWidth && 0 <= y && y < Game1.worldHeight) {
                return _map[y,x];
            } else {
                Console.WriteLine("Woops! Value out of bounds!");
                return 1;
            }
        }

        public Vector2 GetSpawnPoint() {
            return _spawnPoint;
        }



    }

}