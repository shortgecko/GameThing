using Microsoft.Xna.Framework.Input;

namespace Frankenweenie
{
    public class VirtualButtonKeyboard : VirtualButton
    {
        private Keys Key;
        private KeyboardState previous;
        private KeyboardState current;

        public VirtualButtonKeyboard(Keys input)
        {
            this.Key = input;
        }


        public override void EarlyUpdate()
        {
            current = Keyboard.GetState();
        }

        public override void LateUpdate()
        {
            previous = current;
        }

        public override bool Pressed
        {
            get
            {
                return current.IsKeyDown(Key) && previous.IsKeyUp(Key);
            }
        }

        public override bool Released
        {
            get
            {
                return current.IsKeyUp(Key) && previous.IsKeyDown(Key);
            }
        }
    }

}