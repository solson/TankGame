using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace TankGame
{
    public class TankGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tankTexture;
        Sprite tank;

        public TankGame() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferMultiSampling = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            tank = new Sprite(tankTexture);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tankTexture = Content.Load<Texture2D>("TankBaseTile.png");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 stick = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None).ThumbSticks.Left;
            float x = stick.X;
            float y = -stick.Y;
            float magnitude = (float)Math.Sqrt(x * x + y * y);

            if (magnitude > 0.4)
            {
                frame = (frame + 1) % 6;
                tank.Speed = 3;
                tank.Heading = (float)(Math.Atan2(y, x));
            }
            else
            {
                tank.Speed = 0;
            }

            tank.Advance();

            base.Update(gameTime);
        }

        int frame = 0;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            int index = frame < 3 ? 0 : 1;
            Rectangle sourceRect = new Rectangle(62 * index, 0, 62, 100);

            spriteBatch.Begin();
            spriteBatch.Draw(tankTexture, tank.Position, sourceRect, Color.White, (float)(tank.Heading - Math.PI / 2),
                new Vector2(32, 50), 1f, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
