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
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;
        Texture2D TankTexture;
        AnimatedSprite Tank;

        public TankGame() : base()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.IsFullScreen = true;
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferMultiSampling = true;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            Tank = new AnimatedSprite(TankTexture, 1, 4);
            Tank.X = GraphicsDevice.Viewport.Width / 2;
            Tank.Y = GraphicsDevice.Viewport.Height / 2;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            TankTexture = Content.Load<Texture2D>("Images/TankBaseTile4Green.png");
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
                Tank.Speed = 3;
                Tank.Heading = (float)(Math.Atan2(y, x));
                Tank.Animating = true;
            }
            else
            {
                Tank.Speed = 0;
                Tank.Animating = false;
            }

            Tank.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();
            Tank.Draw(SpriteBatch);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
