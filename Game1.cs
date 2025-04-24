using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DalePetJump;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch; 
    private TileTextureManager tileTextureManager;
    public static EnvironmentManager environmentManager; // static since we will need it for player
    public static Player player; // this is single player and we just need to acess it once in another class so making it static is probbaly better but if you wanna pass it through you do you
    public const int gameResWidth = 800;
    public const int gameResHeight = 600;
    public const int tileSizeWidth = 16;
    public const int tileSizeHeight = 16;

    // 16x16 tiles or whatever is set to tileSizeWidth/tileSizeHeight
    public const int worldWidth = 50; 
    public const int worldHeight = 38;

    public int[,] map = Defaults.DefaultMap;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);

        _graphics.PreferredBackBufferWidth = gameResWidth;
        _graphics.PreferredBackBufferHeight = gameResHeight;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        tileTextureManager = new TileTextureManager(Content, GraphicsDevice);

        environmentManager = new EnvironmentManager(map);

        environmentManager.SpawnPlatforms();

        player = new Player(_spriteBatch, Content, environmentManager.GetSpawnPoint());

        base.Initialize();
    }

    protected override void LoadContent()
    {
        tileTextureManager.LoadAllTextures();
        
        player.LoadTextures();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        player.Update();

        environmentManager.Update();

        base.Update(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        player.Draw();

        for (int y = 0; y < worldHeight; y++)
        {
            for (int x = 0; x < worldWidth; x++) 
            {
        
                if (map[y,x] > 0) {;
                    _spriteBatch.Draw(tileTextureManager.GetTexture(map[y,x]), new Rectangle ((int) x * tileSizeWidth, (int) y * tileSizeHeight, tileSizeWidth, tileSizeHeight), Color.White);
                }

            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
