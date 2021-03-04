using Microsoft.Xna.Framework.Input;

namespace Pinecorn
{
    public abstract class VirtualButton
    {
        protected bool hasPressed = false;
        public abstract bool Pressed();

        public abstract bool Released();
    }

}