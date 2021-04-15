using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Frankenweenie
{
    public class Animator : Component
    {
        public Dictionary<string, Animation> Animations;
        public Texture2D Frame;

        public float Timer;

        public int CurrentFrame;

        public bool IsPlaying;

        public void Play(string anim, bool loop = false)
        {
            Animation animation = null;
             Animations.TryGetValue(anim, out animation);

            if ( CurrentFrame < animation.Frames.Count)
            {
                 IsPlaying = true;
                //values
                var frame = animation.Frames[ CurrentFrame];
                var frameLen = animation.FrameSpeeds[ CurrentFrame] * Engine.Delta;

                if (frameLen == 1)
                {
                    Timer = 0;
                    Frame = frame;
                    CurrentFrame++;
                }
                else
                {
                    //if frame len greater then 1
                    Timer += Engine.Delta;

                    //if timer is less then the frame len, continue playing the same frame
                    if (Timer < frameLen)
                    {
                        Frame = animation.Frames[CurrentFrame];
                    }
                    else
                    {
                        CurrentFrame++;
                        Frame = frame;
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
            if (IsPlaying)
                Drawer.Batch.Draw(Frame, entity.position, Color.White);
        }
    }
}