using System;
using Microsoft.Xna.Framework;
using Frankenweenie;

namespace Game
{
    public class Timer : Component
    {
        private float m_Duration =0f;
        public float Duration
        {
            get 
            {
                return m_Duration;    
            }
        }


        public void Start(float duration)
        {
            m_Duration = 0f;
            m_Duration = duration;
        }

        public override void Update()
        {
            if(m_Duration > 0 )
            {
                m_Duration -= Engine.Delta;
            }
        }

        public void Clear()
        {
            m_Duration = 0f;
        }
    }
}