﻿using Frankenweenie;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    public static class Input
    {
        public static VirtualInputAxis Horizontal;
        public static VirtualAxis Vertical;
        public static VirtualInputButton Jump;
        public static VirtualInputButton Pause;
        public static VirtualInputButton EditorPlace;

        static Input()
        {
            Horizontal = new VirtualInputAxis()
            {
                Axis1 = new VirtualAxisGamepadLeftX(),
                Axis2 = new VirtualAxisKeyboard(new VirtualButtonKeyboard(Keys.D), new VirtualButtonKeyboard(Keys.A)),
            };
            Vertical = new VirtualInputAxis()
            {
                Axis1 = new VirtualAxisGamepadLeftY(),
                Axis2 = new VirtualAxisKeyboard(new VirtualButtonKeyboard(Keys.S), new VirtualButtonKeyboard(Keys.W)),
            };
            Jump = new VirtualInputButton()
            {
                Input1 = new VirtualButtonKeyboard(Keys.Z),
                Input2 = new VirtualButtonGamepad(Buttons.A),
            };
            Pause = new VirtualInputButton()
            {
                Input1 = new VirtualButtonKeyboard(Keys.Escape),
                Input2 = new VirtualButtonGamepad(Buttons.Start),
            };


        }
    }
}