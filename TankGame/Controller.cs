using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TankGame
{
    class Controller
    {
        PlayerIndex Player { get; set; }
        Vector2 LeftStick { get { return GamePad.GetState(Player, GamePadDeadZone.None).ThumbSticks.Left; } }
        Vector2 RightStick { get { return GamePad.GetState(Player, GamePadDeadZone.None).ThumbSticks.Right; } }

        public Controller(PlayerIndex playerIndex)
        {
            Player = playerIndex;
        }

        public float LeftMagnitude()
        {
            return (float)Math.Sqrt(LeftStick.X * LeftStick.X + (-LeftStick.Y) * (-LeftStick.Y));
        }

        public float RightMagnitude()
        {
            return (float)Math.Sqrt(RightStick.X * RightStick.X + (-RightStick.Y) * (-RightStick.Y));
        }

        public float LeftHeading()
        {
            return (float)(Math.Atan2(-LeftStick.Y, LeftStick.X));
        }

        public float RightHeading()
        {
            return (float)(Math.Atan2(-RightStick.Y, RightStick.X));
        }
    }
}
