using Microsoft.Xna.Framework;
using Frankenweenie;

namespace  Frankenweenie
{
    public class Collider : Component
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


        public static bool Intersects(Collider hitbox, Collider other)
        {
            return other.Left < hitbox.Right &&
                hitbox.Left < other.Right &&
                other.Top < hitbox.Bottom &&
                hitbox.Top < other.Bottom;
        }

        public static bool Intersects(Collider hitbox, Rectangle other)
        {
            return other.Left < hitbox.Right &&
                hitbox.Left < other.Right &&
                other.Top < hitbox.Bottom &&
                hitbox.Top < other.Bottom;
        }


        public Rectangle ToRect()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public void Draw(Color color)
        {
            Drawer.HollowRectangle(ToRect(), 1, color);
        }



    }

}
