using Microsoft.Xna.Framework;
using Pinecorn;

namespace Game
{
    public class Mover : Component
    {
        public Vector2 Velocity;
        public Hitbox Hitbox;

        public float MoveX = 0f;
        public float MoveY = 0f;
        public bool OnGround = false;

        public override void Update()
        {
            
            this.Velocity.X = MoveX;
            this.Velocity.Y = MoveY;

            Hitbox = Entity.Get<Hitbox>();

            foreach(var solid in Level.Solids)
            { 
                if(CheckX(solid))
                    this.Velocity.X = 0;
                if(CheckY(solid))
                    this.Velocity.Y = 0;
                if(MoveY != 0)
                    OnGround = this.Velocity.Y == 0;
                        
            }

            Entity.Position += Velocity;
            Velocity = Vector2.Zero;
        }

        public bool CheckX(Hitbox solid)
        {
            return ((this.Velocity.X > 0 && this.Hitbox.IsTouchingLeft(solid, this)) || (this.Velocity.X < 0 & this.Hitbox.IsTouchingRight(solid, this)));
        }

        public bool CheckY(Hitbox solid)
        {
            return ((this.Velocity.Y > 0 && this.Hitbox.IsTouchingTop(solid, this)) || (this.Velocity.Y < 0 & this.Hitbox.IsTouchingBottom(solid, this)));
        }



    }
}
