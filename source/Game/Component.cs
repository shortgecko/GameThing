using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class Component
    {
        public Entity entity;
        public bool Active = false;
        
        public virtual void Initialize() {}
        public virtual void Update() { }
        public virtual void Render() { }
        
    }
}
