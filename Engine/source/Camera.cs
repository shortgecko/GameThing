using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class Camera
    {
        private Matrix Transform;
        public Matrix Matrix { get { return Transform; } }

        public float Zoom;
        public Vector2 Position;
        public Rectangle Bounds;

        public Camera(Viewport viewport)
        {
            Bounds = viewport.Bounds;
            Zoom = 1f;
            Position = Vector2.Zero;
        }

        public void Update()
        {
            //Transform = Matrix.CreateTranslation()
        }
    }
}
