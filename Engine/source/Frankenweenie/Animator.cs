using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Frankenweenie
{
    [Pooled]
    public class Animator : Component
    {
        public Dictionary<string, Animation> Animations;
        public Texture2D Frame;

        public float Timer;

        public int CurrentFrame;

        public bool IsPlaying;


        public override void Render()
        {
            if (IsPlaying)
                Drawer.Batch.Draw(Frame, Entity.Position, Color.White);
        }
    }
}