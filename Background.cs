using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace asteroids
{
    public class Background
    {
        public Texture2D texture;
        public Vector2 positionStart, positionNext;

        //Settings
        public int speed;
        public int bgWidth;
        public int bgHeight;

        public Background()
        {
            texture = null;

            speed = (int)Settings.Properties.bgSpeed;
            bgWidth = (int)Settings.Properties.screenWidth;
            bgHeight = (int)Settings.Properties.screenHeight;

            positionStart = new Vector2(0, 0);
            positionNext = new Vector2(-bgWidth, 0);
        }

        public void Update(GameTime gameTime)
        {
            positionStart.X += speed;
            positionNext.X += speed;

            if(positionStart.X >= bgWidth)
            {
                positionStart.X = 0;
                positionNext.X = -bgWidth;
            }
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("aBackground");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle( (int)positionStart.X, (int)positionStart.Y, bgWidth, bgHeight), Color.White);
            spriteBatch.Draw(texture, new Rectangle( (int)positionNext.X, (int)positionNext.Y, bgWidth, bgHeight), Color.White);
        }

    }
}
