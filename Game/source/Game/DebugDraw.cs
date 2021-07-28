using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frankenweenie;
using Microsoft.Xna.Framework;

namespace Game
{
    public static class DebugDraw
    {
        public static void Draw()
        {
            for(int x = 0; x < Level.Tiles.Width; x++)
                for(int y = 0; y < Level.Tiles.Height; y++)
                {
                    if(Level.Tiles[x,y] != -1)
                    {
                      Drawer.HollowRectangle(new Rectangle(x * 8, y * 8, 8,8), 2, Color.Green);
                    }
                }

        }
    }
}
