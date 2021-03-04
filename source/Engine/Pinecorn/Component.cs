using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinecorn
{
    public class Component
    {
        public Entity Entity;
        public bool Active = false;

        public virtual void Initialize() {}
        public virtual void Update() { }
        public virtual void Render() { }
        
    }
}
