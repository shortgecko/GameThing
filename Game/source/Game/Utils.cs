using Microsoft.Xna.Framework;

namespace Game
{
    public static class Utils
    {
        public static Rectangle RectF(float x, float y, float width, float height) =>  new Rectangle((int) x, (int) y, (int) width, (int) height);
        public static Rectangle RectF(Vector2 position, float width, float height) => new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
        public static Rectangle RectF(Vector2 position, Vector2 size) => new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
    }

}
