using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frankenweenie;
using System;
using System.Collections.Generic;

namespace Game
{
    
    public class EntityManager
    {
        public static string[] Names =
        {
            "player",
        };

        public static Entity Create(string name, Dictionary<string,object> values = null)
        {
            switch(name)
            {
                case "player":
                    return Player.Create();
                case "tracer":
                    return Tracer.Create();
            }
            throw new Exception("Entity was null");
        }

        
    }
}