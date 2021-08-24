using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Frankenweenie
{
    public class Player : Entity
    {
        public Player()
        {
            Add<PlayerComponent>();
        }
    }

    public class PlayerComponent : Component
    {
        public override void Render()
        {
            Rectangle rectangle = new Rectangle((int)Entity.Position.X, (int)Entity.Position.Y, 48, 48);
            Drawer.Rect(rectangle, Color.Red);
        }
    }
}
