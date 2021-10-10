using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frankenweenie
{

    public struct Frame
    {
        public VTexture Texture;
        public float Duration;

        public Frame(float legnth, VTexture texture)
        {
            Texture = texture;
            Duration = legnth;
        }
    }

    public class Animation : Component
    {
        private List<Frame> m_Frames;
        public List<Frame> Frames => m_Frames;
        private string m_Name;
        public string Name => m_Name;
        private float Timer;
        private int CurrentFrame = 0;
        private Sprite sprite;

        public Animation(string name, List<Frame> frames)
        {
            m_Frames = frames;
            m_Name = name;
        }

        public Animation(string name)
        {
            m_Frames = new List<Frame>();
            m_Name = name;

        }

        public override void Initialize()
        {
            sprite = Entity.Get<Sprite>();
        }

        public void Add(Frame frame)
        {
            m_Frames.Add(frame);
        }

        public void Play()
        {
            Timer += Engine.Delta;

            Frame frame = Frames[CurrentFrame];

            if (Timer > frame.Duration)
            {
                Timer = 0f;

                CurrentFrame++;

                if (CurrentFrame >= Frames.Count)
                    CurrentFrame = 0;
            }

            sprite.Texture = frame.Texture;
        }

            
    }

    [Pooled]
    public class Sprite : Component
    {
        public static int MaxLayerDepth = 100000;
        public VTexture Texture;
        public int Width;
        public int Height;
        public Vector2 Position { get { return Entity.Position; } }
        public Vector2 DrawOffset = Vector2.Zero;
        public Rectangle SourceRectangle;
        public Color Color = Color.White;
        public float Rotation = 0f;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = new Vector2(1, 1);
        public SpriteEffects Effects = SpriteEffects.None;
        public float LayerDepth = 1f;
        public float Timer;
        private Dictionary<string, Animation> Animations;

        public override void Initialize()
        {
            
        }

        public void Play(string animationName)
        {

        }


        public override void Render()
        {
            if(Texture != null)
                Drawer.Batch.Draw(Texture.Texture, Position + DrawOffset, new Rectangle(0,0,Texture.Width, Texture.Height), Color, Rotation, Origin, Scale, Effects, LayerDepth / MaxLayerDepth);
        }

        public override void Removed()
        {
            //Animations.Clear();
        }
    }
}
