using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Frankenweenie
{
    public struct Animator
    {
        public Dictionary<string, Animation> Animations;
        public Texture2D Frame;

        public float Timer;

        public int CurrentFrame;

        public bool IsPlaying;
    }

    public class AnimationSystem : System
    {
        public Animator Animator;

        public override void Update()
        {

        }

        public static void Play(string anim, bool loop = false)
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
            foreach(var entity in World.Buckets[typeof(Animator)].Entities)
            {
                var animator = Entity.get<Animator>(entity);
                var position = Entity.get<Vector2>(entity);

                if (animator.IsPlaying)
                    Drawer.Batch.Draw(animator.Frame, position, Color.White);

            }
        }
    }
}