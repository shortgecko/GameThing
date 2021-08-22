using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Frankenweenie
{
    public class VSong : IDisposable
    {
        private Song Song;
        public TimeSpan Duration;
        
        public VSong(Song song)
        {
            Song = song;
            Duration = song.Duration;
        }

        public void Play()
        {
            MediaPlayer.Play(Song);
        }

        public void Dispose()
        {
            Song.Dispose();
        }
    }
}
