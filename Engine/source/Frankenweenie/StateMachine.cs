using System;
using System.Collections.Generic;

namespace Frankenweenie
{
    public class StateMachine<T> : Component
    {
        private Dictionary<T, Action> InitializeStates = new Dictionary<T, Action>();
        private Dictionary<T,Action> UpdateStates = new Dictionary<T, Action>();
        private Dictionary<T, Action> EndStates = new Dictionary<T, Action>();

        private T currentState;
        private bool BeginState = false;
        private bool EndState;
        private T NextState;
        public T State => currentState;

        public void Add(T state, Action innit = null, Action update = null, Action end = null)
        {
            Logger.Log("adding");
            if (update == null)
                throw new Exception("Update cannot be null!");

            UpdateStates.Add(state, update);

            if (innit != null)
                InitializeStates.Add(state, innit);
            if (end != null)
                EndStates.Add(state, end);

            Logger.Log($"{state.ToString()} {UpdateStates.Count}");
        }

        static bool HasState(Dictionary<T,Action> states, T state)
        {
            states.TryGetValue(state, out Action action);
            if (action == null)
                return false;
            return true;
        }


        public override void Update()
        {
            if(UpdateStates.Count > 0)
            {

                if (!BeginState && HasState(InitializeStates, currentState))
                {
                    InitializeStates[currentState].Invoke();
                    BeginState = true;
                }


                UpdateStates[currentState].Invoke();

                if (EndState && HasState(EndStates, currentState))
                {
                    EndStates[currentState].Invoke();
                    Reset();
                    currentState = NextState;                
            
                }
            }
        }

        private void Reset()
        {
            BeginState = false;
            EndState = false;
        }
        
        public void Set(T state)
        {
            currentState = state;
            Reset();
        }

        public void End(T nextState)
        {
            NextState = nextState;
            EndState = true;
        }

    }

    
}
