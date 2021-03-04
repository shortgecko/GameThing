using System;
using Microsoft.Xna.Framework;

namespace Pinecorn
{
    public class Timer : Component
    {
        private bool StartTimer = false;
        private bool StopTimer = false;
        private bool PauseTimer = false;
        private bool CheckClamp = false;
        private float ClampVal = 0f;
        private float Time = 0;

        public void Clamp(float amount)
        {
            CheckClamp = true;
            ClampVal = amount;
        }

        public bool IsRunning()
        {
            return PauseTimer = false && Time > 0f;
        }
        public float Get()
        {
            return Time;
        }
        public void Start()
        {
            StartTimer = true;
            StopTimer = false;
            PauseTimer = false;
        }

        public void Stop()
        {
            StopTimer = true;
            StartTimer = false;
            PauseTimer = false;
        }

        public void Pause()
        {
            PauseTimer = true;
            StopTimer = false;
            StartTimer = false;
        }
        public override void Update()
        {
            if(StartTimer)
            {
                if(CheckClamp == true)
                {
                    if(Time <= ClampVal)
                    {
                        Time += Engine.DeltaTime;
                    }
                }
                else
                    Time += Engine.DeltaTime;
            }
            if(StopTimer)
            {
                Time = 0f;
            }
        }

        public void Log()
        {
            System.Console.WriteLine("Time:" + Time);
        }
    }
}