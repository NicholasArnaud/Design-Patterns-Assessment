using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMAssessment
{
    class GameManager
    {
        //Singleton class
        private static GameManager instance = null;
        private GameManager()
        {
            //Constructor
        }
        public static GameManager Instance
        {
            get { if (instance == null) { instance = new GameManager(); } return instance; }
        }

        public Combat combat;
        public TurnManager turnManager;
        public Player CurrentPlayer;
        public Player CurrentEnemy;
        public GenFSM<TurnStates> stateSystem;
        public string currentState;
        public List<Player> Players;
    }
}