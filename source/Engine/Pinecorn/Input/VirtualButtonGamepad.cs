using Microsoft.Xna.Framework.Input;

namespace Frankenweenie
{
    public class VirtualButtonGamepad : VirtualButton
    {
        private Buttons Button;
        private int Index;

        public VirtualButtonGamepad(Buttons button, int index = 0)
        {
            Button = button;
            Index = index;
        }
        public override bool Pressed()
        {
            if (Engine.GamePads[Index].IsButtonDown(Button))
                return true;
            return false;

        }
        public override bool Released()
        {
            return Engine.GamePads[Index].IsButtonUp(Button) && Pressed();
        }
    }

}