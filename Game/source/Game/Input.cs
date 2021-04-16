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
            Horizontal.add(new VirtualAxisGamepadLeftX());
            Horizontal.add(new VirtualAxisKeyboard(Keys.D, Keys.A));

            Vertical.add(new VirtualAxisGamepadLeftY());
            Vertical.add(new VirtualAxisKeyboard(Keys.S, Keys.W));

            Jump.add(new VirtualButtonKeyboard(Keys.Z));
            Jump.add(new VirtualButtonGamepad(Buttons.A));

            Pause.add(new VirtualButtonKeyboard(Keys.Escape));
            Pause.add(new VirtualButtonGamepad(Buttons.Start));

            TempRestart.add(new VirtualButtonGamepad(Buttons.X));
            TempRestart.add(new VirtualButtonKeyboard(Keys.Escape));

            WallClimb.add(new VirtualButtonGamepad(Buttons.RightTrigger));

            Reload.add(new VirtualButtonKeyboard(Keys.F5));

            Frankenweenie.Input.add(Horizontal);
            Frankenweenie.Input.add(Vertical);
            Frankenweenie.Input.add(Jump);
            Frankenweenie.Input.add(Pause);
            Frankenweenie.Input.add(WallClimb);
            Frankenweenie.Input.add(Reload);

        }
    }
}