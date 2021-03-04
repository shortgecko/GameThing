using Pinecorn;
using System.Collections.Generic;
using System;

namespace Game
{
    public static class Factory
    {
        public static Entity Load(string name, Microsoft.Xna.Framework.Vector2 Position)
        {
            switch(name)
            {
                case "player":
                    return Player.Create(Position);
            }

            throw new Exception("Entity does not exist");
        }
    }
}
