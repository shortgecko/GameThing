using Microsoft.Xna.Framework.Input;

namespace Frankenweenie
{
    public class VirtualAxisGamepadLeftX : VirtualAxis
    {
        protected override float GetAxis
        {
            get
            {
                return Engine.GamePads[0].ThumbSticks.Left.X;
            }
        }
    }

    public class VirtualAxisGamepadLeftY : VirtualAxis
    {
        protected override float GetAxis
        {
            get
            {
                return (Engine.GamePads[0].ThumbSticks.Left.Y) * -1;
            }
        }
    }

    public class VirtualAxisGamePadDPadX : VirtualAxis
    {
        private int index;

        public VirtualAxisGamePadDPadX(int Index = 0)
        {
            index = Index;
        }

        protected override float GetAxis
        {
            get
            {
                if (Engine.GamePads[index].IsButtonDown(Buttons.DPadRight))
                    return 1;
                if (Engine.GamePads[index].IsButtonDown(Buttons.DPadLeft))
                    return -1;
                return 0;
            }
        }
    }

    public class VirtualAxisGamePadDPadY : VirtualAxis
    {
        private int index;

        public VirtualAxisGamePadDPadY(int Index = 0)
        {
            index = Index;
        }
        
        protected override float GetAxis
        {
            get
            {
                if (Engine.GamePads[index].IsButtonDown(Buttons.DPadDown))
                    return 1;
                if (Engine.GamePads[index].IsButtonDown(Buttons.DPadUp))
                    return -1;
                return 0;
            }
        }
    }
}
