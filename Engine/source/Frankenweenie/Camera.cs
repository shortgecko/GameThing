using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Frankenweenie
{ 
    public class Camera
    {

        private Matrix Matrix;
        public Vector2 Position;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;

        public Matrix Transform
        {
            get
            {
                return 
                Matrix.Identity *
                Matrix.CreateTranslation(new Vector3(-new Vector2((int)Math.Floor(Position.X), (int)Math.Floor(Position.Y)), 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Scale) *
                Matrix.CreateTranslation(new Vector3(new Vector2((int)Math.Floor(Origin.X), (int)Math.Floor(Origin.Y)), 0));

            }
        }

    }
}
