using Microsoft.Xna.Framework.Input;
using Pinecorn;

namespace Game
{
    public static class Input
    {
        public static VirtualAxis Horizontal;
        public static VirtualAxis Vertical;
        public static VirtualButton Jump;
        static Input()
        {
            Horizontal = new VirtualAxisKeyboard(new VirtualButtonKeyboard(Keys.Right), new VirtualButtonKeyboard(Keys.Left));
            Vertical = new VirtualAxisKeyboard(new VirtualButtonKeyboard(Keys.Down), new VirtualButtonKeyboard(Keys.Up));
            Jump = new VirtualButtonKeyboard(Keys.Z);
        }

        public static void Update()
        {
            if(Engine.GamePads[0].IsConnected)
            {
                Logger.Log("CONTROLLER CONNTECTED");
                Jump = new VirtualButtonGamepad(Buttons.A);
            }
        }

    }
}
