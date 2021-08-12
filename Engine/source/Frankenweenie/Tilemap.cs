using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;


namespace Frankenweenie
{
    public class Tilemap : Collider
    {
        public static Entity Create(int[] data, int width, int height, Point size, Tileset tileset)
        {
            Entity entity = new Entity();
            var tilemap = (Tilemap)entity.Add(new Tilemap(data, width, height, size));

            tilemap.Tileset = tileset;
            return entity;
        }

        public new int Width;
        public new int Height;
        public Point CellSize;
        public int[] Cells;
        public float LayerDepth = 1f;
        private RenderTarget2D RenderData;
        private List<Hitbox> Hitboxes;

        public Tileset Tileset;

        public Tilemap(int[] data, int width, int height, Point size)
        {
            Cells = data;
            Width = width;
            Height = height;
            CellSize = size;
            Hitboxes = new List<Hitbox>();

            for(int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    if(data[x + y * Width] != -1)
                     Hitboxes.Add(new Hitbox(x * 8, y * 8, 8, 8));
                }
        }


        //private void RenderTexture(Texture2D Texture)
        //{
        //    for (int x = 0; x < Width; x++)
        //        for (int y = 0; y < Height; y++)
        //        {
        //            int id = Cells[x + y * Width];

        //            if (id > -1)
        //                Drawer.Batch.Draw(Texture, new Rectangle(x * CellSize.X, y * CellSize.Y, CellSize.X, CellSize.Y), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, LayerDepth / Sprite.MaxLayerDepth);
        //        }
        //}

        public int this[int i] => Cells[i];
        public int this[int x, int y] => Cells[x + y *Width];

        public bool Contains(int x, int y)
        {
            bool lessX = x < 0;
            bool lessY = y < 0;
            bool biggerX = x > Width - 1;
            bool biggerY = y > Height - 1;

            if (lessX || lessY || biggerX || biggerY)
                return false;

            return true;
        }

        public override CollisionData Check(Point offset, Collider other)
        {
            foreach(var hitbox in Hitboxes)
            {
                if (other.Check(offset, hitbox) != null)
                {
                    return new CollisionData(true, Entity, offset);
                }
            }

            return null;
        }

        public override void Render()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    int id = Cells[x + y * Width];
                    if (id > -1)
                        Tileset.Draw(id, new Point(x, y), Color.White, LayerDepth / Sprite.MaxLayerDepth);
                }
        }

    }
}
