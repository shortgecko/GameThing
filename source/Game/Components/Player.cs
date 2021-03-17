using Pinecorn;
using Microsoft.Xna.Framework;

namespace Game
{
    public class Player : Component
    {   
        public static Entity Create()
        {
            Entity player = new Entity();
            player.add<Player>();
            player.Name = "player";
            return player;
        }

        private const float speed = 550f;
        private Timer fireRateTimer;
        private float fireRate = 0.07f;

        public override void Initialize()
        {
            entity.add(fireRateTimer = new Timer());
            
        }

        public override void Update()
        {
            entity.position.X += Input.Horizontal.GetAxis() * speed * Engine.Delta;
            entity.position.Y += Input.Vertical.GetAxis() * speed * Engine.Delta;

            if(Input.Shoot.Pressed() && fireRateTimer.Duration <= 0)
            {
                entity.World.Add(Bullet.Create(new Vector2(entity.position.X , entity.position.Y)));
                entity.World.Add(Bullet.Create(new Vector2(entity.position.X + 4, entity.position.Y)));
                fireRateTimer.Start(fireRate);
            }


        }

        public override void Render()
        {
            Asset.DrawRectangle(new Rectangle((int)entity.position.X, (int)entity.position.Y, 8, 8), Color.White);
        }
    }

}