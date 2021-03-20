using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Frankenweenie
{
    public static class MouseInput
    {
        public static Vector2 Position = new Vector2(200, 200);
        public static MouseState MouseState;
        private static MouseState PreviousState;
        public static Rectangle MouseRectangle;
        

        public static void Update()
        {
            MouseRectangle = new Rectangle(MouseState.X, MouseState.Y, 5, 5);
            PreviousState = MouseState;
            MouseState = Mouse.GetState(); 
            Position.X = MouseState.X;  
            Position.Y = MouseState.Y;
        }
    }
}
