using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Frankenweenie
{
    public static class VirtualMouse
    {
        public static Vector2 Position = new Vector2(200, 200);
        public static MouseState State;
        private static MouseState PreviousState;
        public static Rectangle Rect;
        public static float Scale = 1f; /*Mouse scale, for matrix, so the mouse is always relative to
        the screen */


        public static void Update()
        {
            PreviousState = State;
            State = Mouse.GetState();
            Position = Vector2.Transform(new Vector2(State.X, State.Y), Matrix.Invert(Matrix.CreateScale(Scale)));
            Rect = new Rectangle((int)Position.X, (int)Position.Y, 1, 1);
        }
    }
}
