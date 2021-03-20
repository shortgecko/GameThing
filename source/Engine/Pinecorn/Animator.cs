using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Frankenweenie
{
    public class Animator : Component
    {
        Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();

        public Texture2D Frame;

        private float Timer = 0;

        private int CurrentFrame = 0;

        private bool IsPlaying = false;

        public override void Update()
        {

        }
        public void Add(Animation anim)
        {
            Animations.Add(anim.Name, anim);
        }

        public void Play(string anim, bool loop = false)
        {
            Animation animation = null;
            Animations.TryGetValue(anim, out animation);

            if(CurrentFrame < animation.Frames.Count)
            {
                IsPlaying = true;
                //values
                var frame = animation.Frames[CurrentFrame];
                var frameLen = animation.FrameSpeeds[CurrentFrame] * Engine.Delta;

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
            else if(loop)
            {
                CurrentFrame = 0;
            }

        }

        public override void Render()
        {
            if(IsPlaying)
                Drawer.Batch.Draw(Frame, this.entity.position, Color.White);
        }
    }
}