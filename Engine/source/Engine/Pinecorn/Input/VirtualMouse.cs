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
