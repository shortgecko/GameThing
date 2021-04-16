using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Frankenweenie;

namespace Game
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

        public Hitbox Hitbox(Point Position)
        {
            int x = Position.X / 8 * 8;
            int y = Position.Y / 8 * 8;
            int coordinates = x + y * Width;
            return new Hitbox(x, y, 8, 8);
        }
        

        public void Render()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    if (Data[x + y * Width] > -1)
                        Drawer.Rect(new Rectangle(x * 8, y * 8, 8, 8), Color.White);
                }
        }
    }
}
