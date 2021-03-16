using Pinecorn;
using Microsoft.Xna.Framework;

namespace Game
{
    public class Player : Component
    {   
        public static Entity Create(Vector2 Pos)
        {
            Entity player = new Entity();
            player.add<Player>();
            player.get<Player>().spawn = Pos;
            player.add<StateMachine>();
            player.add<Mover>();
            player.add(new Hitbox(0,0,8,8));
            player.position = Pos;
            player.Name = "player";
            return player;
        }

        public Vector2 spawn;
        private StateMachine stateMachine;
        private Mover mover;
        private float speed = 96f;
        private float gravity = 144f;
        private const float jumpForce = -700f;
        private const float jumpBufferMax = 20f;
        private const float hJumpForce = 71f;
        private Timer jumpBufferTimer;	
	
        public override void Initialize()
        {
            stateMachine = entity.get<StateMachine>();
            mover = entity.get<Mover>();
            stateMachine.add(0,null,NormalUpdate,null);
            entity.add(jumpBufferTimer = new Timer());
        }
        public void NormalUpdate()
        {
            //Regular moving
            mover.moveX = Input.Horizontal.GetAxis() * speed * Engine.Delta;
            Logger.Log("DELTA " + Engine.Delta); ;

            mover.moveY += gravity * Engine.Delta;

            if (Input.Jump.Pressed() && mover.onGround)
            {
                mover.moveY = jumpForce * Engine.Delta;
                mover.moveX += hJumpForce *  Engine.Delta;
            }

        }


        public override void Render()
        {
           Asset.DrawRectangle(new Rectangle((int)entity.position.X, (int)entity.position.Y, 8,8), Color.Red);
        }
    }

}