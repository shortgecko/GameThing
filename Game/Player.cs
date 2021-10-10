using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Frankenweenie
{
    public class Player : Entity
    {
        public Player()
        {
            Add<Mover>();
            Add(new BoxCollider2D(0, 0, 48, 48));
            Add<Sprite>();
            Add<PlayerComponent>();

        }
    }

    public class PlayerComponent : Component
    {
        private Mover Mover;
        private Animation animation;
        VirtualInputAxis Vertical;
        VirtualInputAxis Horizontal;
        private float Speed = 1000f;
        private Sprite Sprite;
        private Animation idle;

        public override void Initialize()
        {
            Mover = Entity.Get<Mover>();
            Sprite = Entity.Get<Sprite>();
            Sprite.Scale = new Vector2(4, 4);
            Sprite.Texture = Content.LoadTexture("1.png");

            animation = (Animation)Entity.Add(new Animation("Anim"));
            animation.Add( new Frame(0.1f, Content.LoadTexture("1.png")));
            animation.Add(new Frame(0.1f, Content.LoadTexture("2.png")));

            Vertical = new VirtualInputAxis();
            Vertical.Add(new VirtualAxisKeyboard(Keys.S, Keys.W));
            Horizontal = new VirtualInputAxis();
            Horizontal.Add(new VirtualAxisKeyboard(Keys.D, Keys.A));
            Input.Add(Horizontal);
            Input.Add(Vertical);

        }

        public override void Update()
        {
            Mover.Move.X += (Horizontal * Speed);
            Mover.Move.Y += (Vertical * Speed);
            if (Mover.Move.X != 0)
            {
                animation.Play();

            }

            Logger.Log(Entity.Position);
            //Mover.Move.Y = Vertical * 500f * Engine.Delta;


        }
        public override void Render()
        {

        }
    }
}
