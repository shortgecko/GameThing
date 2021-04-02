using Frankenweenie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public class Bullet : Component
    {
        public static Entity Create()
        {
            Entity bullet = new Entity();
            bullet.add<Bullet>();
            return bullet;
        }

        public Vector2 position;
        private Texture2D sprite;
        private float airTime = 0f;
        public override void Initialize()
        {
            entity.position = position;
            sprite = Asset.Texture("Graphics/bullet.png");
        }

        public override void Update()
        {
            airTime += Engine.Delta;
            entity.position.Y -= 1200f * airTime * Engine.Delta;
        }

        public override void Render()
        {
            Drawer.Batch.Draw(sprite, entity.position, Color.White);
        }
    }
}
