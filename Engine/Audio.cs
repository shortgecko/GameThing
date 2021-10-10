using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMOD;

namespace Frankenweenie
{
    public static class Audio
    {
        public static FMOD.System API_System;
        public static FMOD.ChannelGroup API_Channel;
        private static RESULT result;


        public static void Innitialize()
        {
            result = FMOD.Factory.System_Create(out API_System);
            ErrorCheck();
            result = API_System.init(512, FMOD.INITFLAGS.NORMAL, IntPtr.Zero);
            ErrorCheck();
            result = API_System.createChannelGroup("Master", out API_Channel);
            ErrorCheck();

        }

        public static void Update()
        {
            API_System.update();
        }
        

        public static void Deinitialize()
        {
            API_Channel.release();
            API_System.release();
        }

        private static void ErrorCheck()
        {
            if (result != RESULT.OK)
            {
                Console.WriteLine(Error.String(result));
            }
        }
    }
}
