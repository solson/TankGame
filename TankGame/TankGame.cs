using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Collections.Generic;

namespace TankGame
{
    public class TankGame : Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch SpriteBatch;
        Texture2D BaseTexture;
        Texture2D TurretTexture;
        Texture2D ShellTexture;
        Tank Player;
        Controller Controller;
        List<Sprite> Bullets;

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

            Player = new Tank(BaseTexture, 1, 4, 35, TurretTexture);
            Player.TankBase.X = GraphicsDevice.Viewport.Width / 2;
            Player.TankBase.Y = GraphicsDevice.Viewport.Height / 2;

            Controller = new Controller(PlayerIndex.One);
            Bullets = new List<Sprite>();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            BaseTexture = Content.Load<Texture2D>("Images/TankBaseTile4Green.png");
            TurretTexture = Content.Load<Texture2D>("Images/TurretGreen.png");
            ShellTexture = Content.Load<Texture2D>("Images/TankShell.png");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Controller.LeftMagnitude() > 0.4)
            {
                Player.TankBase.Speed = (float)(6 * (Controller.LeftMagnitude() - 0.4 ) / 0.6);
                Player.TankBase.Heading = Controller.LeftHeading();
                Player.TankBase.UpdateTime = (int)(Player.TankBase.UpdateFrameTime / (6 * (Controller.LeftMagnitude() - 0.4) / 0.6));
                Player.TankBase.Animating = true;
            }
            else
            {
                Player.TankBase.Speed = 0;
                Player.TankBase.Animating = false;
            }

            if(Controller.RightMagnitude() > 0.4)
            {
                Player.TankTurret.Heading = Controller.RightHeading();
            }

            if(GamePad.GetState(PlayerIndex.One, GamePadDeadZone.None).Triggers.Right >= 0.99)
            {
                Sprite bullet = new Sprite(ShellTexture);
                bullet.Scale = 0.4f;
                bullet.Heading = Player.TankTurret.Heading;
                bullet.Speed = 10;
                //put the bullets at the end of the tank barrel. Not sure why the - 10 is needed
                bullet.X = Player.TankTurret.X + (float)((Player.TankTurret.Texture.Height / 2 + ShellTexture.Height / 2 * bullet.Scale - 10) * Math.Cos(Player.TankTurret.Heading));
                bullet.Y = Player.TankTurret.Y + (float)((Player.TankTurret.Texture.Height / 2 + ShellTexture.Height / 2 * bullet.Scale - 10) * Math.Sin(Player.TankTurret.Heading));
                Bullets.Add(bullet);
            }

            Player.Update();

            foreach(Sprite bullet in Bullets)
            {
                bullet.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            SpriteBatch.Begin();
            Player.TankBase.Draw(SpriteBatch);
            Player.TankTurret.Draw(SpriteBatch);
            foreach(Sprite bullet in Bullets)
            {
                bullet.Draw(SpriteBatch);
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
