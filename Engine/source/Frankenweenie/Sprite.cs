using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{
    public class Sprite : Component
    {
        public Texture2D Texture;
        public int Width;
        public int Height;
        public Vector2 Position { get { return Entity.Position; } }
        public Rectangle SourceRectangle;
        public Color Color = Color.White;
        public float Rotation = 0f;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = new Vector2(1, 1);
        public SpriteEffects Effects = SpriteEffects.None;
        public float LayerDepth = 0f;

        public Dictionary<string, Animation> Animations;
        public float Timer;
        public int CurrentFrame;
        public bool IsPlaying;

        public void Play(string anim, bool loop = false)
        {
            Animation animation = null;
            Animations.TryGetValue(anim, out animation);

            if (CurrentFrame < animation.Frames.Count)
            {
                IsPlaying = true;
                //values
                var frame = animation.Frames[CurrentFrame];
                var frameLen = animation.FrameSpeeds[CurrentFrame] * Engine.Delta;

                if (frameLen == 1)
                {
                    Timer = 0;
                    Texture = frame;
                    CurrentFrame++;
                }
                else
                {
                    //if frame len greater then 1
                    Timer += Engine.Delta;

                    //if timer is less then the frame len, continue playing the same frame
                    if (Timer < frameLen)
                    {
                        Texture = animation.Frames[CurrentFrame];
                    }
                    else
                    {
                        CurrentFrame++;
                        Texture = frame;
                    }
                }
            }
            else if (loop)
            {
                CurrentFrame = 0;
            }

        }

        public override void Render()
        {
            if(Texture != null)
                Drawer.Batch.Draw(Texture, Position, new Rectangle(0,0,Texture.Width, Texture.Height), Color, Rotation, Origin, Scale, Effects, LayerDepth);
        }
    }
}
