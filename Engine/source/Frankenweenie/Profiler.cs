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

        public static void Update()
        {
            if(Engine.Timer > 5)
            {
                Frames.Add(Engine.Delta);
            }
        }

        public static void End()
        {
            GetProfile(out float lowest, out float highest, out float average);
            using (StreamWriter Writer = new StreamWriter("profile"))
            {
                Writer.WriteLine("Profile");
                Writer.WriteLine($"Lowest frametime {lowest * 1000} ms");
                Writer.WriteLine($"Highest frametime {highest * 1000} ms");
                Writer.WriteLine($"Average framtime {average * 1000} ms");
                Writer.WriteLine($"Time played {Engine.Timer}");
                Writer.Close();
            }
        }

        static void GetProfile(out float lowest, out float highest, out float average)
        {
            if(Frames.Count > 1)
            {
                float n = 0f;
                lowest = Frames.Min();
                highest = Frames.Max();
                Frames.Min();
                foreach (float f in Frames)
                {

                    n += f;
                }

                average = n / Frames.Count;
                return;
            }

            throw new Exception("Game did not run long enough to get profile");
            
        }
    }
}
