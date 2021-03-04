using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Pinecorn
{
    public class StateMachine : Component
    {

        private static int MaxStates = 10;
        private Action[] States = new Action[MaxStates];

        public int State = 0;

        public void AddState(int state, Action action)
        {
            States[state] = action;
        }

        public override void Initialize()
        {

        }
    
        public override void Update()
        {
            States[State].Invoke();
        }
    }
}
