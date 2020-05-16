using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace asteroids
{
    public class Asteroid
    {
        readonly Random seed;

        string[] images;
        readonly string[] mainAxis;
        readonly string[] side;
        readonly string determinedAxis;
        readonly string determinedSide;

        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float angle;
        public float spawnWidth;
        public float spawnHeight;
        public int textureID;
        public int spawnX;
        public int spawnY;

        //Settings
        public int maxWidth;
        public int maxHeight;
        public int spawnOffset;
        public int speed;
        public int size;

        //Collision
        public Rectangle bounds;
        public bool isColliding;
        public bool isDestroyed;

        public Asteroid()
        {
            seed = new Random();

            images = new string[] { "aCircle", "aSpiky", "aSquare", "aTriangle" };
            mainAxis = new string[] { "X", "Y" };
            side = new string[] { "inner", "outer" };

            texture = null;
            isColliding = false;
            isDestroyed = false;

            maxWidth = (int)Settings.Properties.screenWidth;
            maxHeight = (int)Settings.Properties.screenHeight;
            spawnOffset = (int)Settings.Properties.asteroidSpawnOffset;
            speed = (int)Settings.Properties.asteroidSpeed;
            size = (int)Settings.Properties.asteroidSize;

            determinedAxis = mainAxis[seed.Next(mainAxis.GetLength(0))];
            determinedSide = side[seed.Next(side.GetLength(0))];
            textureID = seed.Next(0, images.GetLength(0));

            position = DetermineSpawnPoint(determinedAxis, determinedSide);

        }

        public void Update(GameTime gameTime)
        {
            bounds = new Rectangle((int)position.X, (int)position.Y, size, size);

            position.X += speed;
            position.Y += speed;

            if (position.X > maxWidth)
                position.X = 0;
            if (position.X < 0)
                position.X = maxWidth;
            if (position.Y > maxHeight)
                position.Y = 0;
            if (position.Y < 0)
                position.Y = maxHeight;

            // Rotation
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            angle += elapsed;
            float circle = MathHelper.Pi * 2;
            angle %= circle;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(images[textureID]);
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!isDestroyed)
            {
                spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, size, size),
                    null, Color.White, angle, origin, SpriteEffects.None, 0f);
            }
        }

        public Vector2 DetermineSpawnPoint(string axis, string side)
        {
            if (axis == "X")
            {
                // bottom
                if (side == "inner")
                {
                    spawnX = seed.Next(0, maxWidth);
                    spawnY = maxHeight + spawnOffset;
                }
                // top
                else
                {
                    spawnX = seed.Next(0, maxWidth);
                    spawnY = 0 - spawnOffset;
                }
            }
            else
            {
                // left
                if (side == "inner")
                {
                    spawnX = 0 - spawnOffset;
                    spawnY = seed.Next(0, maxHeight);
                }
                // right
                else
                {
                    spawnX = maxWidth + spawnOffset;
                    spawnY = seed.Next(maxHeight);
                }
            }

            return new Vector2(spawnX, spawnY);
        }
    }
}
