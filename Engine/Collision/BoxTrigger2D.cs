using System;
using Frankenweenie;
using Microsoft.Xna.Framework;

namespace  Frankenweenie
{
    public class BoxTrigger2D : Collider2D
    {
        public Action<Entity> OnTriggerEnter;
        public Action<Entity> OnTriggerLeave;

        public BoxTrigger2D()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }

        public BoxTrigger2D(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public BoxTrigger2D(float x, float y, float width, float height)
        {
            X = (int)x;
            Y = (int)y;
            Width = (int)width;
            Height = (int)height;
        }

        public BoxTrigger2D(Rectangle rectangle)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

    }
}