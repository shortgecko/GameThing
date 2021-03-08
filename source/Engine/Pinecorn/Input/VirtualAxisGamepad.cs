using System;
using Microsoft.Xna.Framework.Input;

namespace Pinecorn
{
    public class VirtualAxisGamepad: VirtualAxis
    {
        public override int GetAxis()
        {
           return (int)Engine.GamePads[0].ThumbSticks.Left.X;
        }
    }
}
