using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace asteroids
{
    public class Asteroids : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Asteroid asteroid = new Asteroid();
        Player player = new Player();
        Background bg = new Background();

        public Asteroids()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = (int)Settings.Properties.screenWidth,
                PreferredBackBufferHeight = (int)Settings.Properties.screenHeight
            };

            this.Window.Title = "Asteroids";
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            asteroid.LoadContent(Content);
            player.LoadContent(Content);
            bg.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            asteroid.Update(gameTime);
            player.Update(gameTime);
            bg.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            bg.Draw(spriteBatch);
            asteroid.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
