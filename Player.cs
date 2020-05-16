using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace asteroids
{
    class Player
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 direction;
        public float accel;
        public float angle;
        public bool isThrusting;

        //Settings
        readonly int width;
        readonly int height;
        readonly float angleChange;
        readonly float ignitionAccel;
        readonly float thrustAccel;
        readonly float maxAccel;

        //Collision
        public bool isColliding;
        public Rectangle bounds;

        public Player()
        {
            texture = null;
            isThrusting = false;
            isColliding = false;

            width = (int)Settings.Properties.screenWidth;
            height = (int)Settings.Properties.screenHeight;
            accel = (float)Settings.Properties.startAccel;
            angle = (float)Settings.Properties.startAngle;
            angleChange = (float)Settings.Properties.angleChangeSpeed;
            ignitionAccel = (float)Settings.Properties.ignitionAccel;
            thrustAccel = (float)Settings.Properties.thrustAccel;
            maxAccel = (float)Settings.Properties.maxSpeed;

            position = new Vector2(width / 2, height / 2); //starting pos should be in the middle of screen
        }

        public void Update(GameTime time)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left))
                angle -= 1 / angleChange;
            if (keyState.IsKeyDown(Keys.Right))
                angle += 1 / angleChange;
            if (keyState.IsKeyDown(Keys.Up))
            {
                if (!isThrusting)
                {
                    isThrusting = true;
                    accel = ignitionAccel;
                }
                direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                position += direction * accel;
                accel += ignitionAccel / thrustAccel;
                if (accel >= maxAccel)
                    accel = maxAccel;
            }
            else
            {
                isThrusting = false;
                if (accel > 0)
                    accel -= 1 / (2 * angleChange);
            }
            position += direction * accel;

            if (accel <= ignitionAccel)
                accel = ignitionAccel;

            if (position.X > width)
                position.X = 0;
            if (position.X < 0)
                position.X = width;
            if (position.Y > height)
                position.Y = 0;
            if (position.Y < 0)
                position.Y = height;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("aSpaceship");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height),
                null, Color.White, angle, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
