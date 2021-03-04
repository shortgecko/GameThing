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
           return Keyboard.GetState().IsKeyDown(Key);
        }

        public override bool Released()
        {
             return Keyboard.GetState().IsKeyUp(Key);
        }
    }

}