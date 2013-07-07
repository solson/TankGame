using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public float Heading { get; set; }

        public Vector2 Velocity
        {
            get
            {
                float velX = Speed * (float) Math.Cos(Heading);
                float velY = Speed * (float) Math.Sin(Heading);
                return new Vector2(velX, velY);
            }
        }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Position = new Vector2(0, 0);
            Speed = 0;
            Heading = 0;
        }

        public void Advance()
        {
            Position += Velocity;
        }
    }
}
