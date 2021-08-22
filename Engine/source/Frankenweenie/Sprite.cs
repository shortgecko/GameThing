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
        public Texture2D Texture;
        public float Duration;

        public Frame(float legnth, Texture2D texture)
        {
            Texture = texture;
            Duration = legnth;
        }
    }

    public class Animation
    {
        private Dictionary<float, Frame> Frames;
        private string m_Name;
        public string Name => m_Name;

        public Animation(string name, Dictionary<float, Frame> frames)
        {
            Frames = frames;
            m_Name = name;
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
