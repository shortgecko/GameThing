using System.Collections.Generic;
using Microsoft.Xna.Framework;



namespace Frankenweenie
{
    public abstract class CollisionLayer
    {
        protected List<BoxCollider2D> Hitboxes;

        public void Add(BoxCollider2D h) => Hitboxes.Add(h);
        public void Remove(BoxCollider2D h) => Hitboxes.Remove(h);
        public void Clear() =>Hitboxes.Clear();

        public abstract bool Check(Point offset);
        public CollisionLayer(List<BoxCollider2D> hitboxes)
        {
            Hitboxes = hitboxes;
        }
    }
}