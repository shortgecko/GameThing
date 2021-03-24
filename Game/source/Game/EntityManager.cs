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
            "bullet",
        };

        public static Entity Create(string name, Dictionary<string,object> values = null)
        {
            switch(name)
            {
                case "player":
                    return Player.Create();
                case "bullet":
                    Entity bullet = new Entity();
                    bullet.add<Bullet>();
                    bullet.add(new EditorComponent(8, 8));
                    return bullet;
            }
#if DEBUG
            return new Entity();
#endif
            throw new Exception("Entity Does not Exist");
        }

        
    }
}