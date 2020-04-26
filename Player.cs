using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace asteroids
{
    class SpaceShip
    {
        bool isDriving;
        readonly Texture2D texture;
        Vector2 position;
        Vector2 direction;
        float accel;
        float angle;
        public SpaceShip(ContentManager content, Vector2 screenCenter)
        {
            texture = content.Load<Texture2D>("aSpaceship");
            position.X = screenCenter.X;
            position.Y = screenCenter.Y;

        }

        public void Update(int screenWidth, int screenHeight)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                angle -= 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                angle += 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (!isDriving)
                {
                    isDriving = true;
                    accel = 1;
                }
                direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                position += direction * accel;
                accel += 0.2f;
                if (accel >= 5)
                    accel = 5;
            }
            else
            {
                isDriving = false;
                accel -= 0.1f;
            }
            position += direction * accel;

            if (accel <= 0)
                accel = 0;

            if (position.X > screenWidth)
                position.X = 0;
            if (position.X < 0)
                position.X = screenWidth;
            if (position.Y > screenHeight)
                position.Y = 0;
            if (position.Y < 0)
                position.Y = screenHeight;

        }
        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Width), null, Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
        }

    }
}