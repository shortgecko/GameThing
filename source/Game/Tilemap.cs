using System;
using Pinecorn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public class Tilemap
    {
        public int[] Data;
        public readonly int Width;
        public readonly int Height;

        public Texture2D Texture;
        
        public Tilemap(int[] data, int width, int height)
        {
            Data = data;
            Width = width;
            Height = height;
        }
        
        public void Render()
        {
            for(int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    if(Data[x + y * Width] > -1)
                        Asset.DrawRectangle(new Rectangle(x * 8, y * 8, 8,8), Color.White);
                }
        }
    }
}