using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Frankenweenie
{
    public class Calc
    {
        public static Vector2 Center(float X, float Y, float Width, float Height)
        {
            return new Vector2(X + (Width / 2), Y + (Height / 2));
        }

        public static Vector2 Center(Vector2 position, float Width, float Height)
        {
            return new Vector2(position.X + (Width / 2), position.Y + (Height / 2));
        }
    }
}
