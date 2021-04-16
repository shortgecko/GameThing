using System.Collections.Generic;

namespace Frankenweenie
{

    public class Component
    {
        public Component()
        {

        }
        public Entity Entity;
        public virtual void Initialize() { }
        public virtual void Update() { }
        public virtual void Render() { }
        public virtual void Removed() { }
    }
}
