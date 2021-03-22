using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Frankenweenie
{
    public class Tileset
    {
        public Rectangle[,] Tiles;
        public Texture2D Source;
        public int TileWidth;
        public int TileHeight;

        public Tileset(Texture2D texture, int tileWidth, int tileHeight)
        {
            Source = texture;
            Tiles = new Rectangle[Source.Width / tileWidth, Source.Height / tileHeight];

            TileWidth = tileWidth;
            TileHeight = tileHeight;

            for (int x = 0; x < tileWidth; x++)
                for (int y = 0; x < tileWidth; y++)
                {
                    Tiles[x, y] = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                }
        }

    }
}