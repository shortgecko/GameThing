using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Component
    {
        public Entity Entity;
        public virtual void Initialize() { }
        public virtual void Update() { }
        public virtual void Render() { }
        public virtual void Removed() { }
    }
}
