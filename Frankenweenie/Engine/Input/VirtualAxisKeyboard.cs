using Microsoft.Xna.Framework.Input;
namespace Frankenweenie
{
    public class VirtualAxisKeyboard : VirtualAxis
    {
        private Keys Positive;
        private Keys Negative;

        public VirtualAxisKeyboard(Keys positive, Keys negative)
        {
            Positive = positive;
            Negative = negative;
        }

        protected override float GetAxis
        {
            get
            {
                if (Keyboard.GetState().IsKeyDown(Positive))
                    return 1;
                if (Keyboard.GetState().IsKeyDown(Negative))
                    return -1;
                return 0;
            }
        }

    }
}
