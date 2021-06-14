using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace Frankenweenie
{
    public class Tilemap
    {
        public int Width;
        public int Height;
        public int[] Data;
        public float LayerDepth = 1f;
        private RenderTarget2D RenderData;
 
        public Tilemap(int[] data, int width, int height)
        {
            Data = data;
            Width = width;
            Height = height;
        }

        private void UploadToData(Tileset Tileset)
        {
            var GraphicsDevice = Engine.Device.GraphicsDevice;
            RenderData = new RenderTarget2D(GraphicsDevice, Width, Height);
            GraphicsDevice.SetRenderTarget(RenderData);
            Drawer.Batch.Begin(SpriteSortMode.FrontToBack);
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    int id = Data[x + y * Width];
                    if (id > -1)
                        Tileset.Draw(id, new Point(x, y), Color.White, LayerDepth);
                }
            Drawer.Batch.End();
            GraphicsDevice.SetRenderTarget(null);
        }



        public void Render(Tileset Tileset)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    int id = Data[x + y * Width];
                    if (id > -1)
                        Tileset.Draw(id, new Point(x , y ), Color.White, LayerDepth);
                }
        }


        public void Render(Texture2D Texture)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    int id = Data[x + y * Width];

                    if (id > -1)
                        Drawer.Batch.Draw(Texture, new Rectangle(x * 8, y * 8, 8, 8), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepth);
                }
        }

        public int this[int i] => Data[i];

    }
}
