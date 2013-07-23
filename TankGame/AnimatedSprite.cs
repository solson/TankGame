using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    public class AnimatedSprite : Sprite
    {
        public int Rows { get; set; }
        public int Row { get; private set; }
        public int Columns { get; set; }
        public int Column { get; private set; }
        public int FrameWidth { get { return Texture.Width / Columns; } }
        public int FrameHeight { get { return Texture.Height / Rows; } }
        public Boolean Animating { get; set; }
        public int UpdateTime { get; set; }
        public int UpdateFrameTime { get; private set; }

        private Rectangle SourceRect;
        private Stopwatch Timer;
        private long TimeSinceUpdate;
        

        public AnimatedSprite(Texture2D texture, int rows, int columns, int updateFrameTime) : base(texture)
        {
            Rows = rows;
            Columns = columns;
            Row = 1;
            Column = 1;
            Timer = new Stopwatch();
            Timer.Start();
            TimeSinceUpdate = 0;
            UpdateFrameTime = updateFrameTime;
        }

        public override void Update()
        {
            base.Update();
            if(Animating)
            {
                if(Timer.ElapsedMilliseconds - TimeSinceUpdate > UpdateTime)
                {
                    NextFrame();
                    TimeSinceUpdate = Timer.ElapsedMilliseconds;
                }
            }
            SourceRect = new Rectangle(FrameWidth * (Column - 1), FrameHeight * (Row - 1),
                    Texture.Width / Columns, Texture.Height / Rows);
        }

        private void NextFrame()
        {
            Column++;
            if(Column > Columns)
            {
                Row++;
                Column = 1;
            }
            if(Row > Rows)
            {
                Row = 1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRect, Color.White, (float)(Heading + Math.PI / 2),
                new Vector2(FrameWidth / 2, FrameHeight / 2), 1f, SpriteEffects.None, 0);
        }
    }
}
