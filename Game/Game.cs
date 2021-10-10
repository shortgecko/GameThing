using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frankenweenie.Game
{
    public class Game : Scene
    {
        public AudioFile vSong;

        protected override void Initialize()
        {
            World.CreateCamera();

            vSong = Content.LoadSong("on_sight.mp3");

        }

        protected override void Update()
        {
            vSong.Play();
            vSong.SetVolume(100f);

        }
    }
}
