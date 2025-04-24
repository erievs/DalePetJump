using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace DalePetJump {
    public class TileTextureManager {
        private List<Texture2D> textures = [];
        private ContentManager _contentManager;
        private GraphicsDevice _graphicsDevice;

        public TileTextureManager(ContentManager contentManager, 
        GraphicsDevice graphicsDevice) {


            _contentManager = contentManager;

            _graphicsDevice = graphicsDevice;

        }

        public void LoadAllTextures() {

            if(_graphicsDevice == null)  {
                throw new NullReferenceException();
            }
            

            // Tile 0 (we need to fill this space up so let's just do a blank one)

            var blankTexture = new Texture2D(_graphicsDevice, 1, 1);
            textures.Add(blankTexture);

            // Tile 1 (we need to fill this space up so let's just do a blank one)

            var tilePlatform1 = _contentManager.Load<Texture2D>("platform");
            textures.Add(tilePlatform1);

        }

        public Texture2D GetTexture(int id) {

            return textures[id];

        }


    }

}