using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    public class Tank
    {
        public AnimatedSprite TankBase { get; set; }
        public Sprite TankTurret { get; set; }

        public Tank(Texture2D tankBase, int rows, int columns, int updateTime, Texture2D tankTurret)
        {
            TankBase = new AnimatedSprite(tankBase, rows, columns, updateTime);
            TankTurret = new Sprite(tankTurret);
        }

        public void Update()
        {
            TankBase.Update();
            //To offset the turret (the turret isn't in the middle of the tank)
            TankTurret.X = TankBase.X + 7 * (float) Math.Cos(TankBase.Heading);
            TankTurret.Y = TankBase.Y + 7 * (float) Math.Sin(TankBase.Heading);
        }
    }
}
