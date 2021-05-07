using Microsoft.Xna.Framework;
using Frankenweenie;

namespace Game
{
    public class Hitbox : Component
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public int Left
        {
            get { return this.X; }
        }

        public int Right
        {
            get { return (this.X + this.Width); }
        }

        public int Top
        {
            get { return this.Y; }
        }

        public int Bottom
        {
            get { return (this.Y + this.Height); }
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} Width: {Width} Height: {Height}";
        }

        public Hitbox(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static bool Intersects(Hitbox hitbox, Hitbox other)
        {
            return other.Left < hitbox.Right &&
                hitbox.Left < other.Right &&
                other.Top < hitbox.Bottom &&
                hitbox.Top < other.Bottom;
        }



    }

}
