using System;
using System.Collections.Generic;

namespace Frankenweenie
{
    public class StateMachine<T> : Component
    {
        //public int MaxStates = 10;

        public Dictionary<T,Action> InitializeStates;
        public Dictionary<T,Action> UpdateStates;
        public Dictionary<T, Action> EndStates;
        public T State;
        private bool Begun = false;
        private bool End = false;

        public void add(T state, Action innit = null, Action update = null, Action end = null)
        {
            if (update == null)
                throw new Exception("Update cannot be null!");
            if(innit != null)
                InitializeStates.Add(state, innit);
            if (end != null)
                EndStates[state] = end;
            UpdateStates.Add(state, update);
            if (UpdateStates.Count < 1)
                State = state;
        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            if (!Begun && InitializeStates[State] != null)
            {
                InitializeStates[State].Invoke();
                Begun = true;
            }

            UpdateStates[State].Invoke();

            if (End)
            {
                EndStates[State].Invoke();
                End = false;

                //Clear states
                Begun = false;
                End = false;
            }

        }

        public void EndState()
        {
            End = true;
        }
    }
}
