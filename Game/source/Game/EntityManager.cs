using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frankenweenie;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game
{
    
    public class EntityManager
    {
        public static Entity Create(string name, Parameters p)
        {
            switch(name)
            {
                case "player":
                    return Player.Create();
                case "platform":
                    return Platform.Create(p);
            }
            throw new Exception("Entity was null");
        }

        
    }
}