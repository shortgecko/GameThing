using Frankenweenie;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    public static class Input
    {
        public static VirtualInputAxis Horizontal = new();
        public static VirtualInputButton Jump = new();
        public static VirtualInputButton Pause = new();
        public static VirtualInputButton TempRestart = new();

        static Input()
        {
            Horizontal.add(new VirtualAxisGamepadLeftX());
            Horizontal.add(new VirtualAxisKeyboard(Keys.D, Keys.A));

            Jump.add(new VirtualButtonKeyboard(Keys.Z));
            Jump.add(new VirtualButtonGamepad(Buttons.A));

            Pause.add(new VirtualButtonKeyboard(Keys.Escape));
            Pause.add(new VirtualButtonGamepad(Buttons.Start));

            TempRestart.add(new VirtualButtonGamepad(Buttons.X));
            TempRestart.add(new VirtualButtonKeyboard(Keys.Escape));

            Frankenweenie.Input.add(Horizontal);
            Frankenweenie.Input.add(Jump);
            Frankenweenie.Input.add(Pause);

        }
    }
}