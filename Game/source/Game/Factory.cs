using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frankenweenie;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game
{
    
    public class Factory
    {
        private static void Add(Entity e) => World.Add(e);


        public static void Entity(string name, Vector2 Position, Parameters Parameters)
        {
         
            switch(name)
            {
                case "player":
                    Add(Player.Create(Position));
                    break;
                case "spinner":
                    Add(Spinner.Create(Position, Parameters));
                    break;
                case "spring":
                    Add(Spring.Create(Position));
                    break;
                default:
                    return;
            }
        }


        public static Entity Trigger(string name, Point position, Parameters p)
        {
            switch(name)
            {
                case "trigger":
                    return new Entity();
            }
            throw new Exception("Trigger not found");
        }



        
    }
}