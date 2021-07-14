using Frankenweenie;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    public static class Input
    {
        public static VirtualInputAxis Horizontal = new();
        public static VirtualInputAxis Vertical = new();
        public static VirtualInputButton Jump = new();
        public static VirtualInputButton WallClimb = new();
        public static VirtualInputButton Pause = new();
        public static VirtualInputButton TempRestart = new();
        public static VirtualInputButton Reload = new();

        static Input()
        {
            Horizontal.Add(new VirtualAxisGamepadLeftX());
            Horizontal.Add(new VirtualAxisKeyboard(Keys.D, Keys.A));
            Horizontal.Add(new VirtualAxisKeyboard(Keys.Right, Keys.Left));

            Vertical.Add(new VirtualAxisGamepadLeftY());
            Vertical.Add(new VirtualAxisKeyboard(Keys.W, Keys.S));
            Vertical.Add(new VirtualAxisKeyboard(Keys.Down, Keys.Up));


            Jump.Add(new VirtualButtonKeyboard(Keys.Z));
            Jump.Add(new VirtualButtonKeyboard(Keys.Space));
            Jump.Add(new VirtualButtonGamepad(Buttons.A));

            Pause.Add(new VirtualButtonKeyboard(Keys.Escape));
            Pause.Add(new VirtualButtonGamepad(Buttons.Start));

            TempRestart.Add(new VirtualButtonGamepad(Buttons.X));
            TempRestart.Add(new VirtualButtonKeyboard(Keys.Escape));

            WallClimb.Add(new VirtualButtonGamepad(Buttons.RightTrigger));
            WallClimb.Add(new VirtualButtonKeyboard(Keys.X));

            Reload.Add(new VirtualButtonKeyboard(Keys.F5));

            Frankenweenie.Input.Add(Horizontal);
            Frankenweenie.Input.Add(Vertical);
            Frankenweenie.Input.Add(Jump);
            Frankenweenie.Input.Add(Pause);
            Frankenweenie.Input.Add(WallClimb);
            Frankenweenie.Input.Add(Reload);

        }
    }
}