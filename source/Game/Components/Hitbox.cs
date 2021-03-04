using Microsoft.Xna.Framework;
using Pinecorn;

namespace Game
{
    public class Hitbox : Component
    {
        private float PX;
        private float PY;

        private int Width;
        private int Height;

        public Hitbox(float posX, float posY, int width, int height)
        {
            PX = posX;
            PY = posY;

            Width = width;
            Height = height;
            Bounds = new Rectangle((int)posX, (int)posY, width, height);
        }

        public override void Update()
        {
            Bounds = new Rectangle((int)Entity.Position.X + (int)PX, (int)Entity.Position.Y + (int)PY, Width, Height);
        }

        public Rectangle Bounds;

        #region Collision

        public bool IsTouchingLeft(Hitbox sprite, Mover _this)
        {
            return _this.Hitbox.Bounds.Right + _this.Velocity.X > sprite.Bounds.Left &&
              _this.Hitbox.Bounds.Left < sprite.Bounds.Left &&
              _this.Hitbox.Bounds.Bottom > sprite.Bounds.Top &&
              _this.Hitbox.Bounds.Top < sprite.Bounds.Bottom;
        }

        public bool IsTouchingRight(Hitbox sprite, Mover _this)
        {
            return _this.Hitbox.Bounds.Left + _this.Velocity.X < sprite.Bounds.Right &&
              _this.Hitbox.Bounds.Right > sprite.Bounds.Right &&
              _this.Hitbox.Bounds.Bottom > sprite.Bounds.Top &&
              _this.Hitbox.Bounds.Top < sprite.Bounds.Bottom;
        }

        public bool IsTouchingTop(Hitbox sprite, Mover _this)
        {
            return _this.Hitbox.Bounds.Bottom + _this.Velocity.Y > sprite.Bounds.Top &&
              _this.Hitbox.Bounds.Top < sprite.Bounds.Top &&
              _this.Hitbox.Bounds.Right > sprite.Bounds.Left &&
              _this.Hitbox.Bounds.Left < sprite.Bounds.Right;
        }

        public bool IsTouchingBottom(Hitbox sprite, Mover _this)
        {
            return _this.Hitbox.Bounds.Top + _this.Velocity.Y < sprite.Bounds.Bottom &&
              _this.Hitbox.Bounds.Bottom > sprite.Bounds.Bottom &&
              _this.Hitbox.Bounds.Right > sprite.Bounds.Left &&
              _this.Hitbox.Bounds.Left < sprite.Bounds.Right;
        }

        #endregion


    }

}
//hahahaha funny number