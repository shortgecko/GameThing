namespace Frankenweenie
{
    public class Autotiler
    {
        public static void Tile(Tilemap tilemap)
        {
            for(int x = 0; x < tilemap.Width; x++)
                for(int y = 0; y < tilemap.Height; y++)
                {
                    int i = tilemap.Data[x + y * tilemap.Width];
                    if (i > -1)
                        tilemap.Data[x + y * tilemap.Width] = Set(tilemap, x, y);
                }
        }              

        private static int Set(Tilemap tilemap, int x, int y)
        {
            int north = Get(tilemap, x, y - 1);
            int south = Get(tilemap, x, y + 1);
            int east = Get(tilemap, x + 1, y);
            int west = Get(tilemap, x - 1, y);
            return north + 2 * west + 4 * east + 8 * south;
        }

        private static int Get(Tilemap tilemap, int x, int y)
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