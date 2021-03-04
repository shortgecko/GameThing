using Microsoft.Xna.Framework.Input;

namespace Pinecorn
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
            if(Keyboard.GetState().IsKeyDown(Key))
            {
                hasPressed = true;
                return true;
            }
            return false;
        }

        public override bool Released()
        {
            if(Keyboard.GetState().IsKeyUp(Key) && hasPressed)
            {
                hasPressed = false;
                return true;
            }
            return false; 
        }
    }

}