using Microsoft.Xna.Framework;
using Frankenweenie;

namespace  Frankenweenie
{
    public class Collider2D : Component
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


        public bool Intersects( Collider2D other)
        {
            return other.Left < Right &&
                Left < other.Right &&
                other.Top < Bottom &&
                Top < other.Bottom;
        }

        public bool Intersects(Collider2D hitbox, Rectangle other)
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

        public virtual CollisionData Check(Point offset, Collider2D other)
        {
            return null;
        }



    }

}
