using Microsoft.Xna.Framework.Input;
namespace Pinecorn
{
    public class VirtualAxisKeyboard: VirtualAxis
    {
        private VirtualButton Positive;
        private VirtualButton Negative;
        public VirtualAxisKeyboard(VirtualButton positive, VirtualButton negative)
        {
            Positive = positive;
            Negative = negative;
        }

        public override int GetAxis()
        {
            if (Positive.Pressed())
                return 1;
            if (Negative.Pressed())
                return -1;
            return 0;
        }
    }
}
