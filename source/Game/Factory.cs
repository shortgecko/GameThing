using Pinecorn;
using System.Collections.Generic;
using System;

namespace Game
{
    public static class Factory
    {
        
        public static Entity Player(World world)
        {
            var player = new Entity();
            player.World = world;
            player.Add<Mover>();
            player.Add<Player>();
            player.Add <Animator>();
            player.Add<StateMachine>();
            player.Add<Timer>();    
            player.Add(new Hitbox(0,0,48,48));
            player.Name = "player";
            return player;
        }
    }
}
