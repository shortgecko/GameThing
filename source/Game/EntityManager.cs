using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pinecorn;
using System;
using System.Collections.Generic;

namespace Game
{
    public class EntityManager
    {
        
        public static Entity Create(string name, Vector2 pos, int width, int height, Dictionary<string,object> values)
        {
            switch(name)
            {
                case "player":
                    return Player.Create(pos,pos);
            }
            return new Entity();
        }
    }
}