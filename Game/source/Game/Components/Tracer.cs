using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game
{
    public class Tracer : Component
    {
        public static Entity Create()
        {
            Entity tracer = new Entity();
            tracer.add<Tracer>();
            tracer.Name = "tracer";
            return tracer;
        }

        private Texture2D texture;

        public override void Initialize()
        {
            texture = Asset.Texture("graphics/tracer.png");
        }

        public override void Render()
        {
            Drawer.Batch.Draw(texture, new Rectangle((int)entity.position.X, (int)entity.position.Y, texture.Width, texture.Height), Color.White);

        }
    }
}
