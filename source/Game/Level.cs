using System;
using System.Collections.Generic;
using Pinecorn;

namespace Game
{
    public static class Level
    {   
        public static List<Hitbox> Solids = new List<Hitbox>();
        
        public static List<Entity> Entities = new List<Entity>();
        public static Tilemap FG_Tiles;

        public static void Render()
        {
            FG_Tiles.Render();
        }

        public static void Clear()
        {
            
        }
                
    }
}