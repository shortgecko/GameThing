using System.Collections.Generic;

namespace Frankenweenie
{
    public class Component
    {
        public Entity entity;
        public virtual void Initialize() { }
        public virtual void Update() { }
        public virtual void Render() { }
    }
}
