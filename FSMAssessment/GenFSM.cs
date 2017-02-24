using System.Collections.Generic;

namespace FSMAssessment
{

    public class GenFSM
    {
        public List<string> States; //List of states
        public List<string> Transitions; //List of transitions 
        public string CurrentState; //Current state the fsm is in

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initialState">The starting state of the program</param>
        public GenFSM(string initialState)
        {
            States = new List<string>();
            Transitions = new List<string>();
            AddState(initialState);
        }

        /// <summary>
        /// Adds a state to the list of states and if its already in the list it
        /// won't be added
        /// </summary>
        /// <param name="state">Added state to the states list</param>
        void AddState(string state)
        {
            if (!States.Contains(state.ToUpper()))
                States.Add(state.ToUpper());
        }

        /// <summary>
        /// Creates a transition of two states if the transition isn't already defined
        /// and the two states are valid inside the list of states
        /// </summary>
        /// <param name="from">Current state</param>
        /// <param name="to">Destination state</param>
        /// <param name="reversed">Adds a return for the states in list</param>
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

        /// <summary>
        /// Checks the list of states and then checks to see if the transition is 
        /// valid from the current state to the next state then will change to the destination state
        /// </summary>
        /// <param name="goal">State to reach</param>
        public void TryTransition(string goal)
        {
            if (States.Contains(goal.ToUpper()))
            {
                string transition = CreateTransition(CurrentState, goal);
                if (Transitions.Contains(transition))
                    CurrentState = goal;
            }
        }

        /// <summary>
        /// Adds a transition to the list of transitions
        /// </summary>
        /// <param name="transition">Transition to add to list</param>
        private void TryAddTransition(string transition)
        {
            if (!Transitions.Contains(transition))
                Transitions.Add(transition);
        }

        /// <summary>
        /// Creates a transition as strings
        /// </summary>
        /// <param name="from">The current state</param>
        /// <param name="to">The destination state</param>
        /// <returns></returns>
        private string CreateTransition(string from, string to)
        {
            return from.ToUpper() + ">" + to.ToUpper();
        }
    }
}