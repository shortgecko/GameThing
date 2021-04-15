using Microsoft.Xna.Framework.Input;

namespace Frankenweenie
{
    public class VirtualButtonGamepad : VirtualButton
    {
        private Buttons Button;
        private GamePadState previous;
        private GamePadState current;
        private int Index;

        public VirtualButtonGamepad(Buttons button, int index = 0)
        {
            Button = button;
            Index = index;
        }

        public override void EarlyUpdate()
        {
            current = Engine.GamePads[Index];
        }

        public override void LateUpdate()
        {
            previous = current;
        }

        public override bool Pressed
        {
            get
            {
                return current.IsButtonDown(Button) && previous.IsButtonUp(Button);
            }

        }
        public override bool Released
        {
            get
            {
                return current.IsButtonUp(Button) && previous.IsButtonDown(Button);
            }
        }
    }

}