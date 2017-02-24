using System.Collections.Generic;

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

        /// <summary>
        /// Singleton referencer
        /// </summary>
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        //Objects to be called from the class throughout the project
        public Combat Combat; //Singleton call for Combat FSM 
        public TurnManager turnManager; //Singleton call for TurnManager class
        public Player CurrentPlayer; //Singleton call for current player
        public Player CurrentEnemy; //Singleton call for current enemy
        public GenFSM GenFSM; //Singleton call for Generic FSM
        public List<Player> Players; //Singleton call for list of players
    }
}