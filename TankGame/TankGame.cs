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
        Texture2D BaseTexture;
        Texture2D TurretTexture;
        Tank Player;

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

            Player = new Tank(BaseTexture, 1, 4, 20, TurretTexture);
            Player.TankBase.X = GraphicsDevice.Viewport.Width / 2;
            Player.TankBase.Y = GraphicsDevice.Viewport.Height / 2;
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            BaseTexture = Content.Load<Texture2D>("Images/TankBaseTile4Green.png");
            TurretTexture = Content.Load<Texture2D>("Images/TurretGreen.png");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 leftStick = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None).ThumbSticks.Left;
            Vector2 rightStick = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None).ThumbSticks.Right;
            float leftX = leftStick.X;
            float leftY = -leftStick.Y;
            float rightX = rightStick.X;
            float rightY = -rightStick.Y;
            float leftMagnitude = (float)Math.Sqrt(leftX * leftX + leftY * leftY);
            float rightMagnitude = (float)Math.Sqrt(rightX * rightX + rightY * rightY);

            if(leftMagnitude > 0.4)
            {
                Player.TankBase.Speed = 6;
                Player.TankBase.Heading = (float)(Math.Atan2(leftY, leftX));
                Player.TankBase.Animating = true;
            }
            else
            {
                Player.TankBase.Speed = 0;
                Player.TankBase.Animating = false;
            }

            if(rightMagnitude > 0.4)
            {
                Player.TankTurret.Heading = (float)(Math.Atan2(rightY, rightX));
            }

            Player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            SpriteBatch.Begin();
            Player.TankBase.Draw(SpriteBatch);
            Player.TankTurret.Draw(SpriteBatch);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
