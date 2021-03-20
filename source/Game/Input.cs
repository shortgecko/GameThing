using Frankenweenie;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    public static class Input
    {
        public static VirtualInputAxis Horizontal;
        public static VirtualAxis Vertical;
        public static VirtualInputButton Shoot;
        public static VirtualInputButton Pause;
        static Input()
        {
            Horizontal = new VirtualInputAxis()
            {
                Axis1 = new VirtualAxisGamePadDPadX(),
                Axis2 = new VirtualAxisKeyboard(new VirtualButtonKeyboard(Keys.D), new VirtualButtonKeyboard(Keys.A)),
            };
            Vertical = new VirtualInputAxis()
            {
                Axis1 = new VirtualAxisGamePadDPadY(),
                Axis2 = new VirtualAxisKeyboard(new VirtualButtonKeyboard(Keys.S), new VirtualButtonKeyboard(Keys.W)),
            };
            Shoot = new VirtualInputButton()
            {
                Input1 = new VirtualButtonKeyboard(Keys.Z),
                Input2 = new VirtualButtonGamepad(Buttons.RightTrigger),
            };
            Pause = new VirtualInputButton()
            {
                Input1 = new VirtualButtonKeyboard(Keys.Escape),
                Input2 = new VirtualButtonGamepad(Buttons.Start),
            };
        }
    }
}