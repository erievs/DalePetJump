using System;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DalePetJump {


    public class Player {
        
        private SpriteBatch _spriteBatch;
        private ContentManager _contentManager;
        private Texture2D _dalesPetTexture;
        public Vector2 _position;
        public Vector2 xSpeed = new Vector2 (2.5f, 0);
        public Vector2 ySpeed = new Vector2 (0, 2.5f);
        public float jumpHeight = 5f;
        int jumpCounter = 0;
        private int maxJumpTime = 60; // 60 times a second 
        int jumpCoolDownCounter = 0;
        private int maxJumpCoolDown = 60;
        private JUMP_STATUS jumpStatus = JUMP_STATUS.JUMPING;
        private bool isFalling = false;

        public Player(SpriteBatch spriteBatch, ContentManager contentManager, Vector2 position) {

            _spriteBatch = spriteBatch;

            _contentManager = contentManager;

            _position = position;
        }

        public void LoadTextures() {

            _dalesPetTexture = _contentManager.Load<Texture2D>("dale_pet");

        }

        public void Update() {

            KeyboardState keyboardState = Keyboard.GetState();

            if(isFalling) {
                _position = _position + ySpeed;
            }

            // we need to 'tile' our position (well it is easier and better to do)

            int tiledX = (int) _position.X / Game1.tileSizeWidth;

            int tiledY = (int) _position.Y / Game1.tileSizeHeight;

            Console.WriteLine("Tiled Player Cords " + new Vector2(tiledX, tiledY));

            // since tiledY is the top of the player we need to subtract 2 tiles to get the the tile under the player.

            // it has 2 is 2 tiles, so 16x16 

            // also 0 is air! 

            // the player is 32x32 so we must check two tiles!

            if(Game1.environmentManager.GetTile(tiledX, tiledY + 2) > 0 || 
               Game1.environmentManager.GetTile(tiledX + 1, tiledY + 2) > 0  ) {
                isFalling = false;
            } else {
                isFalling = true;
            }


            // for jumping like in doodle jump
            // we need a small cool down so we really fall like in doodle jump
            // you can use a for loop, had some minor issues with a for loop
            // if you want to make it a for loop you can do it I was just lazy

            if(jumpStatus == JUMP_STATUS.JUMPING) {

                jumpCounter++; //(60 times per second)

                _position.Y -= jumpHeight; // in XNA subtracting in Y goes up, while adding goes down!

                if(jumpCounter == maxJumpTime) {
                    jumpStatus = JUMP_STATUS.NOT_JUMPING;
                    jumpCounter = 0;
                }

            }

            if(jumpStatus == JUMP_STATUS.NOT_JUMPING) {

                jumpCoolDownCounter++; //(60 times per second)

                if(jumpCoolDownCounter == maxJumpCoolDown) {
                    jumpStatus = JUMP_STATUS.JUMPING;
                    jumpCoolDownCounter = 0;
                }

            }

            // this is to handle new 'levels' as you go up
            // remember that XNA in the y axis has - going up + down.
            // so 0 would be the top of the level
            // and 50 or whatever the worldHeigh is is the bottom
            // we want to tp the player when they reach the top clear and spawn new set of platforms

            if(tiledY < 1) {
                
                Game1.environmentManager.ClearPlatforms();
                Game1.environmentManager.SpawnPlatforms();
                _position = Game1.environmentManager.GetSpawnPoint();

            }

            if(keyboardState.IsKeyDown(Keys.D)) {
                _position = _position + xSpeed;
            } 

            if(keyboardState.IsKeyDown(Keys.A)) {
                _position = _position - xSpeed;
            } 



        }

        public void Draw() {

            _spriteBatch.Draw(_dalesPetTexture, new Vector2(_position.X, _position.Y), Color.White);

        }

    }

}