using System;

namespace Frankenweenie
{
    public class StateMachine : Component
    {
        public int MaxStates = 10;
        public Action[] InitializeStates;
        public Action[] UpdateStates;
        public Action[] EndStates;
        public int State = 0;
        private bool Begun = false;
        private bool End = false;
        public StateMachine()
        {
            InitializeStates = new Action[MaxStates];
            UpdateStates = new Action[MaxStates];
            EndStates = new Action[MaxStates];
        }

        public StateMachine(int maxStates)
        {
            MaxStates = maxStates;
            InitializeStates = new Action[MaxStates];
            UpdateStates = new Action[MaxStates];
            EndStates = new Action[MaxStates];
        }
        public void add(int state, Action innit = null, Action update = null, Action end = null)
        {
            if (update == null)
                throw new Exception("Update cannot be null!");
            InitializeStates[state] = innit;
            UpdateStates[state] = update;
            EndStates[state] = end;
            if (UpdateStates[State] == null)
                throw new Exception();
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
