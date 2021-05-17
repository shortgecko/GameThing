using Frankenweenie;
using Microsoft.Xna.Framework;
namespace Game
{
    public class Hitbox : Collider
    {
        public bool TriggerOverlap = false;

        public Hitbox(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

    }

}