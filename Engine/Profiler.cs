using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Frankenweenie
{
    public static class Profiler
    {
        private static List<float> Frames = new List<float>();

        public static void Update(float frame_rate)
        {
           Frames.Add(frame_rate);
        }

        public static void End()
        {
            GetProfile(out float lowest, out float highest, out float average, out float p);
            using (StreamWriter Writer = new StreamWriter("profile"))
            {
                Writer.WriteLine("Engine Profile");
                Writer.WriteLine("--------------------------------------------------------------------");
                Writer.WriteLine($"Lowest framerate {lowest } fps");
                Writer.WriteLine($"Highest framerate {highest } fps");
                Writer.WriteLine($"Average frametime {average } fps");
                Writer.WriteLine($"Percentage game hit 60fps {p / Frames.Count * 100 }%");
                Writer.WriteLine($"Percentage game did not hit 60fps {100 - (p / Frames.Count * 100) }%");
                Writer.WriteLine($"Time played {Engine.Timer}s");
                Writer.Close();
            }
        }

        static void GetProfile(out float lowest, out float highest, out float average, out float p)
        {
            if(Frames.Count > 1)
            {
                float n = 0f;
                lowest = Frames.Min();
                highest = Frames.Max();

                foreach (float f in Frames)
                {

                    n += f;
                }

                average = n / Frames.Count;

                float number_of_times_hit_60 = 0;

                foreach(float f in Frames)
                {
                    if(f >= 60)
                    {
                        number_of_times_hit_60++;
                    }
                }

                p = number_of_times_hit_60;

                return;
            }

            Logger.Log("Game did not run long enough to get profile");
            lowest = 0f;
            highest = 0f;
            average = 0f;
            p = 0f;
        }
    }
}
