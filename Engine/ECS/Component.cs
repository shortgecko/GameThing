using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Frankenweenie
{
    public class Component
    {
        [NonSerialized]
        public Entity Entity;
        public virtual void Initialize() { }
        public virtual void Update() { }
        public virtual void Render() { }
        public virtual void Removed() { }
    }
}
