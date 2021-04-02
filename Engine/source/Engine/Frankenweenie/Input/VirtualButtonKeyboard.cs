using Microsoft.Xna.Framework.Input;

namespace Frankenweenie
{
    public class VirtualButtonKeyboard : VirtualButton
    {
        private Keys Key;
        public VirtualButtonKeyboard(Keys input)
        {
            this.Key = input;
        }

        public override bool Pressed()
        {
            if (Keyboard.GetState().IsKeyDown(Key))
            {
                return true;
            }
            return false;
        }

        public override bool Released()
        {
            if (Keyboard.GetState().IsKeyUp(Key) && Pressed())
            {
                return true;
            }
            return false;
        }
    }

}