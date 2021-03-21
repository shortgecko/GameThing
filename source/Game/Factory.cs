using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frankenweenie;
using System;
using System.Collections.Generic;

namespace Game
{
    public class Factory
    {
        
        public static Entity Create(string name, Dictionary<string,object> values = null)
        {
            switch(name)
            {
                case "player":
                    return Player.Create();
            }
#if DEBUG
            return new Entity();
#endif
            throw new Exception("Entity Does not Exist");
        }

        
    }
}