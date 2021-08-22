using System;
using Frankenweenie;
using Microsoft.Xna.Framework;

namespace Frankenweenie
{
    public class CollisionData
    {
        public Entity Entity;
        public bool Collision;
        public Point Offset;

        public CollisionData(bool collison, Entity entity, Point offset)
        {
            Collision = collison;
            Entity = entity;
            Offset = offset;
        }
    }
}
