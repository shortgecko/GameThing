using System;
using System.Collections.Generic;
using System.Text;
using Frankenweenie;

namespace Game
{
    public static class Level
    {
        public static Tilemap Tiles;
        public static Tilemap BgTiles;
        public static List<Hitbox> Solids = new List<Hitbox>();
        public static List<Entity> Entities => World.Entities;
        
    }
}
