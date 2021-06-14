using System;
using Frankenweenie;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Game
{
    [Pooled]
    public class Solid : Component
    {
        private List<Component> Actors = new List<Component>();
        private List<Mover> Riding = new List<Mover>();
        public  Vector2 Move;
        private Hitbox Hitbox;
        private Trigger Trigger;
        public  List<Component> Hitboxes;

        public override void Initialize()
        {
            Actors = World.All<Mover>();
            Hitbox = Entity.Get<Hitbox>();
            base.Initialize();
        }
        
        private bool Check(Point offset, Hitbox other) => Hitbox.Check(offset, Hitbox, other);

        private bool CheckSolids(Point offset)
        {   
             for (int i = 0; i < Level.Solids.Count; i++)
            {
                var hitbox = Level.Solids[i];
                if (Check(offset, hitbox))
                {
                    if (hitbox != this.Hitbox)
                        return true;
                 }
            }
            return false;
        }


        public override void Update()
        {
            // foreach(Mover actor in Actors)
            // {

            //     CheckX(actor);
            // }
            // Move = Vector2.Zero;
        }

        private void CheckX(Mover mover)
        {
            int moveX = (int)Move.X;
            int sign = Math.Sign(Move.X);

            Move.X = (float)Math.Round(Move.X);

            while (Move.X != 0)
            {
                if (!CheckSolids(new Point(sign, 0)))
                {
                    Entity.Position.X += sign * Engine.Delta;
                    Move.X -= sign;
                }
                else
                {
                    break;
                }
            }

            //right
            if(sign == 1)
            {

                if(Riding.Contains(mover))
                {
                    //carry right
                    mover.Move.X += moveX;
                }
                if(Hitbox.Intersects(Hitbox, mover.Hitbox))
                {
                    //push right
                    mover.Move.X += (Hitbox.Right - mover.Hitbox.Left);
                }
            }
            else if(sign == -1)
            {
                
                if(Riding.Contains(mover))
                {
                    //carry left
                    mover.Move.X += mover.Move.X;
                }
                if(Hitbox.Intersects(Hitbox, mover.Hitbox))
                {
                    //push right
                    mover.Move.X += (Hitbox.Left - Hitbox.Right);
                }
            }

            Entity.Position.X += moveX * Engine.Delta;
        }
    }
}
