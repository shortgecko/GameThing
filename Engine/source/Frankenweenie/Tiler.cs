using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie
{
    public class Tiler
    {
        private const int variable = -1;

        public static void Tile(Tilemap tilemap)
        {
            for(int x = 0; x < tilemap.Width; x++)
                for(int y = 0; y < tilemap.Height; y++)
                {
                    int i = tilemap.Data[x + y * tilemap.Width];
                    if (i > -1)
                        tilemap.Data[x + y * tilemap.Width] = GetID(tilemap, x, y);
                }
        }              

        private static int GetID(Tilemap tilemap, int x, int y)
        {
            var north = GetTile(tilemap, x, y - 1);
            var south = GetTile(tilemap, x, y + 1);
            var east = GetTile(tilemap, x + 1, y);
            var west = GetTile(tilemap, x - 1, y);
            var id = north + 2 * west + 4 * east + 8 * south;
            //id -= 1;
            return id;
        }

        private static int GetTile(Tilemap tilemap,int x, int y)
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
