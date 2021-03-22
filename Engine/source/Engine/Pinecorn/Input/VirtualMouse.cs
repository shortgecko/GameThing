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
        public static MouseState State;
        public static Vector2 Position;

        public static void Update()
        {
            State = Mouse.GetState();
            Position = new Vector2(State.X, State.Y);
        }
    }
}
