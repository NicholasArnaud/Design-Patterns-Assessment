using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMAssessment
{

    public class GenFSM
    {
        public List<string> States;
        public List<string> Transitions;
        public string CurrentState;

        public GenFSM(string initialState)
        {
            States = new List<string>();
            Transitions = new List<string>();
            AddState(initialState);
        }

        void AddState(string state)
        {
            if (!States.Contains(state.ToUpper()))
                States.Add(state.ToUpper());
        }

        public void AddTransitions(string from, string to, bool reversed)
        {
            if (States.Contains(from.ToUpper()) && States.Contains(to.ToUpper()))
            {
                string transition = CreateTransition(from, to);
                TryAddTransition(transition);
                if (reversed)
                {
                    transition = CreateTransition(to, from);
                    TryAddTransition(transition);
                }
            }
        }

        public void TryTransition(string goal)
        {
            if (States.Contains(goal.ToUpper()))
            {
                string transition = CreateTransition(CurrentState, goal);
                if (Transitions.Contains(transition))
                    CurrentState = goal;
            }
        }

        private void TryAddTransition(string transition)
        {
            if (!Transitions.Contains(transition))
                Transitions.Add(transition);
        }

        private string CreateTransition(string from, string to)
        {
            return from.ToUpper() + ">" + to.ToUpper();
        }
    }
}