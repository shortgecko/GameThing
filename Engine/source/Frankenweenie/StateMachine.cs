using System;
using System.Collections.Generic;

namespace Frankenweenie
{
    public class StateMachine<T> : Component
    {
        private Dictionary<T, Action> InitializeStates = new();
        private Dictionary<T,Action> UpdateStates = new();
        private Dictionary<T, Action> EndStates = new();
        public T State;
        private bool BeginState = false;
        private bool EndState;
        private T NextState;
     
        public void add(T state, Action innit = null, Action update = null, Action end = null)
        {
            if (update == null)
                throw new Exception("Update cannot be null!");
            if(innit != null)
                InitializeStates.Add(state, innit);
            if (end != null)
                EndStates.Add(state, end);
            UpdateStates.Add(state, update);
            
        }

        private static bool HasState(Dictionary<T,Action> states, T state)
        {
            states.TryGetValue(state, out Action action);
            if (action == null)
                return false;
            else
                return true;
        }

        public override void Update()
        {
            if (!BeginState && HasState(InitializeStates, State))
            {
                InitializeStates[State].Invoke();
                BeginState = true;
            }

            UpdateStates[State].Invoke();

            if(EndState && HasState(EndStates, State))
            {
                EndStates[State].Invoke();
                Reset();
                State = NextState;
            }
        }

        private void Reset()
        {
            BeginState = false;
            EndState = false;
        }
        
        public void Set(T state)
        {
            State = state;
            Reset();
        }

        public void End(T nextState)
        {
            NextState = nextState;
            EndState = true;
        }

    }
}
