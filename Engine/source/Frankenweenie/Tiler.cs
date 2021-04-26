using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Tiler
    {
        public static void Tile(Tilemap tilemap)
        {
            for(int x = 0; x < tilemap.Width; x++)
                for(int y = 0; y < tilemap.Height; y++)
                {
                    int i = tilemap.Data[x + y * tilemap.Width];
                    if (i > -1)
                        tilemap.Data[x + y * tilemap.Width] = set(tilemap, x, y);
                }
        }              

        private static int set(Tilemap tilemap, int x, int y)
        {
            int north = get(tilemap, x, y - 1);
            int south = get(tilemap, x, y + 1);
            int east = get(tilemap, x + 1, y);
            int west = get(tilemap, x - 1, y);
            return north + 2 * west + 4 * east + 8 * south;
        }

        private static int get(Tilemap tilemap, int x, int y)
        {
            bool lessX = x < 0;
            bool lessY = y < 0;
            bool biggerX = x > tilemap.Width - 1;
            bool biggerY = y > tilemap.Height - 1;

            if (lessX || lessY || biggerX || biggerY)
                return 0;
            if (tilemap.Data[x + y * tilemap.Width] > -1)
                return 1;
            return 0;
        }
    }
}
