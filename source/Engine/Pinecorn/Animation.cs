using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Pinecorn
{

    public class Animation
    {
        public string Name;

        public List<Texture2D> Frames = new List<Texture2D>();
        public List<float> FrameSpeeds = new List<float>();

        public Animation(string name)
        {
            Name = name;
        }

        public void AddFrame(Texture2D frame, float duration=1)
        {
            Frames.Add(frame);
            FrameSpeeds.Add(duration);
        }
        public void AddFrame(string file, float duration=1)
        {
            Texture2D frame = Asset.Texture(file);
            Frames.Add(frame);
            FrameSpeeds.Add(duration);
        }
    }
}