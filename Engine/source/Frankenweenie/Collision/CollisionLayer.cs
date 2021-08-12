using System.Collections.Generic;
using Microsoft.Xna.Framework;



namespace Frankenweenie
{
    public abstract class CollisionLayer
    {
        protected List<Hitbox> Hitboxes;

        public void Add(Hitbox h) => Hitboxes.Add(h);
        public void Remove(Hitbox h) => Hitboxes.Remove(h);
        public void Clear() =>Hitboxes.Clear();

        public abstract bool Check(Point offset);
        public CollisionLayer(List<Hitbox> hitboxes)
        {
            Hitboxes = hitboxes;
        }
    }
}