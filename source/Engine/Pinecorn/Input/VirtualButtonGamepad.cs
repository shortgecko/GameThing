using Microsoft.Xna.Framework.Input;

namespace Pinecorn
{
    public class VirtualButtonGamepad : VirtualButton
    {
        private Buttons Button;
        private int Index;
        private bool hasPressed = false;
        public VirtualButtonGamepad(Buttons button, int index = 0)
        {
            Button = button;
            Index = index;
        }
        public override bool Pressed()
        {
            if(Engine.GamePads[Index].IsButtonDown(Button))
                {hasPressed = true; return true; }
            return false;

        }
        public override bool Released()
        {
            return hasPressed && Engine.GamePads[Index].IsButtonUp(Button);
        }
    }

}