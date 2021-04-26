using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class Tileset
    {
        private Rectangle[,] Tiles;
        private Texture2D Source;
        private int TileWidth;
        private int TileHeight;

        public Tileset(string texture, int tileWidth, int tileHeight)
        {
            Source = Content.Texture(texture);
            Tiles = new Rectangle[Source.Width / tileWidth, Source.Height / tileHeight];

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            for (int x = 0; x < Source.Width / tileWidth; x++)
                for (int y = 0; y < Source.Width / tileHeight; y++)
                {
                    Tiles[x, y] = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                }
        }

        public void Draw(int id, Point Position, Color color, float layerDepth)
        {
            int w = Source.Width / TileWidth;
            int h = Source.Height / TileHeight;
            int x = id / w;
            int y = id % w ;
            var tile = Tiles[y, x];
            Drawer.Batch.Draw(Source, new Rectangle(Position.X * TileWidth, Position.Y * TileHeight, TileWidth, TileHeight), tile, Color.White, 0, Vector2.Zero, SpriteEffects.None, layerDepth);
        }

        public void Dispose() => Source.Dispose();

    }
}