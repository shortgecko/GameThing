using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Frankenweenie
{
    public class Tilemap
    {
        public int Width;
        public int Height;
        public int[] Data;
 
        public Tilemap(int[] data, int width, int height)
        {
            Data = data;
            Width = width;
            Height = height;
        }
        
        public void Render(Tileset Tileset)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    int id = Data[x + y * Width];
                    if (id > -1)
                        Tileset.Draw(id, new Point(x , y ), Color.White);
                }
        }

        public void Render(Texture2D Texture)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    int id = Data[x + y * Width];
                    if (id > -1)
                        Drawer.Batch.Draw(Texture, new Rectangle(x * 8, y * 8, 8, 8), Color.White);
                }
        }
    }
}
