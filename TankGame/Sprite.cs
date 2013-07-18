using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }
        public float Heading { get; set; }

        public Vector2 Position { get { return new Vector2(X, Y); } }
        public float VelX { get { return Speed * (float)Math.Cos(Heading); } }
        public float VelY { get { return Speed * (float)Math.Sin(Heading); } }
        public Vector2 Velocity { get { return new Vector2(VelX, VelY); } }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            X = 0;
            Y = 0;
            Speed = 0;
            Heading = 0;
        }

        public virtual void Update()
        {
            X += VelX;
            Y += VelY;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, (float)(Heading + Math.PI / 2),
                new Vector2(Texture.Width / 2, Texture.Height / 2), 1f, SpriteEffects.None, 0);
        }
    }
}
