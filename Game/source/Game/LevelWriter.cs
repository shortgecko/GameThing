using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.IO;

namespace Game.LevelWriter
{
    public class TilemapRef
    {
        public string[] Data;
        public int Width;
        public int Height;
    }

    public class EntityRef
    {
        public string Name;
        public Vector2 Position;
        public Parameters Parameters;
    }

    public class LevelRef
    {
        public TilemapRef BgTiles = new TilemapRef();
        public TilemapRef Solids = new TilemapRef();
        public List<EntityRef> Entities = new List<EntityRef>();
        public List<EntityRef> Triggers = new List<EntityRef>();
    }

}
