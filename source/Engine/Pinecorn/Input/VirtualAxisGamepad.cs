using System;
using Microsoft.Xna.Framework.Input;

namespace Pinecorn
{
    public class VirtualAxisGamepad: VirtualAxis
    {
        private bool Horizontal = false;
        private bool Veritcal = false;
        private int Index;

        public enum Sticks
        {
            Left,
            Right,
        };

        public Sticks Stick;

        public VirtualAxisGamepad(Sticks stick,int dir, int gamePad = 0)
        {
            int direction = Math.Sign(dir);
            Index = gamePad;

            Stick = stick;

            switch(direction)
            {
                case 1:
                    Veritcal = true;
                    break;
                case -1:
                    Horizontal = true;
                    break;
                case 0:
                    throw new Exception("Input int is not valid direction");

            }
        }

        public override int GetAxis()
        {
            switch(Stick)
            {
                case Sticks.Left:
                    return GetLeft();
                case Sticks.Right:
                    return GetRight();
                
            }
            return 0;
        }

        private int GetLeft()
        {
             switch(Horizontal)
            {
                case true:
                    return (int)Engine.GamePads[Index].ThumbSticks.Left.X;

                case false:
                    return (int)Engine.GamePads[Index].ThumbSticks.Left.Y;
            }
        }

        private int GetRight()
        {
             switch(Horizontal)
            {
                case true:
                    return (int)Engine.GamePads[Index].ThumbSticks.Right.X;

                case false:
                    return (int)Engine.GamePads[Index].ThumbSticks.Right.Y;
            }
        }
    }
}
