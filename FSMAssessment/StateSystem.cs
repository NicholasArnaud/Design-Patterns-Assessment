using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMAssessment
{

    public class StateSystem<T>
    {

        Dictionary<string, State> states;

        public StateSystem()
        {
            states = new Dictionary<string, State>();
            var v = Enum.GetValues(typeof(T));
            foreach (var e in v)
            {
                State s = new State(e as Enum);
                states.Add(s.name, s);
            }
        }

        public string Start()
        {
            return GameManager.Instance.currentState = "INIT";
        }

        public void ChangeState(string s)
        {
            GameManager.Instance.currentState = states[s].ToString();
        }
    }
}