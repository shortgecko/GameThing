using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using FMOD;


namespace Frankenweenie
{
    public class AudioFile : IDisposable
    {
        public FMOD.Sound Sound;
        public FMOD.Channel Channel;
        private static RESULT result;

        private static void ErrorCheck()
        {
            if (result != RESULT.OK)
            {
                Console.WriteLine(Error.String(result));
            }
        }

        public AudioFile(string path)
        {           
            result = Audio.API_System.createSound(path, MODE.DEFAULT, out Sound);
            ErrorCheck();
        }

        public void Play()
        {
            result = Audio.API_System.playSound(Sound, Audio.API_Channel, false, out Channel);
            ErrorCheck();
            result = Channel.setChannelGroup(Audio.API_Channel);
            ErrorCheck();
        }

        public void SetVolume(float volume)
        {
            result = Channel.setVolume(volume / 100);
            ErrorCheck();
        }

        public void Dispose()
        {
            Sound.release();
        }
    }
}

