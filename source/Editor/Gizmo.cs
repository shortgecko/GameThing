using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frankenweenie;

namespace Game.Editor
{
    public static class Gizmo
    {
        public static Vector2 Position;
        private static Texture2D Texture;

        static Gizmo()
        {
            Texture = Asset.Texture("Graphics/gizmo.png");
        }
        public static void Render()
        {
            Drawer.Batch.Draw(Texture, Position, Color.White);

        }
    }
}
