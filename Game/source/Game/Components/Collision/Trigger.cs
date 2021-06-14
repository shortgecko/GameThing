using System;
using Frankenweenie;
using Microsoft.Xna.Framework;

namespace Game
{
    public class Trigger : Collider
    {
        public Action<Entity> OnTriggerEnter;
        public Action<Entity> OnTriggerLeave;

        public Trigger()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }

        public Trigger(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Trigger(float x, float y, float width, float height)
        {
            X = (int)x;
            Y = (int)y;
            Width = (int)width;
            Height = (int)height;
        }

        public Trigger(Rectangle rectangle)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

    }
}