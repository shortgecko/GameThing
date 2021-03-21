using Microsoft.Xna.Framework;
using Frankenweenie;

namespace Game
{
    public class Camera
    {
        public Vector2 Position = Vector2.Zero;
        public Matrix Transform { get; private set; }
   
        public void Initialize()
        {
            //Engine.RenderTarget.Width = Engine.Config.Width;
            //Engine.RenderTarget.Height = Engine.Config.Height;
            //Engine.RenderTarget.Scale.X = 1f;

            Transform = Matrix.CreateTranslation(Vector3.Zero) * Matrix.CreateScale(1f);
            //Engine.Transform = Transform;
        }
        public void Update()
        {
            VirtualMouse.Position = Vector2.Transform(new Vector2(VirtualMouse.State.X, VirtualMouse.State.Y), Matrix.Invert(Transform)) / 4f;
        }
    }
}

