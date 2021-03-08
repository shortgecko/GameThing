using Pinecorn;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Game
{
    public static class Factory
    {
        public static Entity Load(string name, Microsoft.Xna.Framework.Vector2 Position, Dictionary<string,object> values)
        {
            switch(name)
            {
                case "player":
                    values.TryGetValue("spawn", out object obj);

                    return Player.Create(Position,(Vector2)obj);
            }

            throw new Exception("Entity does not exist");
        }
    }
}
