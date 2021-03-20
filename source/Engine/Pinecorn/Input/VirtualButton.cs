using Microsoft.Xna.Framework.Input;

namespace Frankenweenie
{
    public abstract class VirtualButton
    {
        public abstract bool Pressed();
        public abstract bool Released();
    }

}