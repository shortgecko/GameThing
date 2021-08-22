using System;
namespace Frankenweenie
{
    public abstract class VirtualButton
    {
        public abstract bool Pressed { get;  }
        public abstract bool Released { get; }
        public abstract void EarlyUpdate();
        public abstract void LateUpdate();
        public static implicit operator bool(VirtualButton button) => button.Pressed;
    }

}