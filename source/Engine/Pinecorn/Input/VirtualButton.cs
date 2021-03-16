using Microsoft.Xna.Framework.Input;

namespace Pinecorn
{
    public abstract class VirtualButton
    {
        public abstract bool Pressed();
        public abstract bool Released();
    }

}