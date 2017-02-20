
using System.Diagnostics;

namespace FSMAssessment
{
    public class TurnManager
    {
        
        int playernum = 0;//The number of the current player in the list

        /// <summary>
        /// Tis but a constructor
        /// </summary>
        public TurnManager()
        {
            //Constructor
        }

        /// <summary>
        /// Function starts game and sets to the IDLE state while
        /// setting up the first two players in the list
        /// </summary>
        public void ToStartUp()
        {

            Debug.WriteLine("Starting Up");
            GameManager.Instance.currentState = "IDLE";
            GameManager.Instance.CurrentPlayer = GameManager.Instance.Players[0];
            GameManager.Instance.CurrentEnemy = GameManager.Instance.Players[1];
        }

        /// <summary>
        /// Function checks between turns if any current player is dead or there are no more players left
        /// </summary>
        public void ToIdle()
        {
            GameManager gm = GameManager.Instance;
            Debug.WriteLine("Waiting...");
            if (gm.CurrentPlayer.IsDead && playernum !=gm.Players.Count -1)
            {
                playernum += 1;
                gm.combat.ChangePlayer(gm.Players[playernum]);
                if (gm.CurrentEnemy.Name == gm.CurrentPlayer.Name || gm.CurrentPlayer.Name == gm.CurrentPlayer.Name && playernum != gm.Players.Count - 1)
                {
                    playernum += 1;
                    gm.combat.ChangePlayer(gm.Players[playernum]);
                }
            }
            if (gm.CurrentEnemy.IsDead && playernum != gm.Players.Count -1)
            {
                playernum += 1;
                gm.combat.ChangeEnemy(gm.Players[playernum]);
                if (gm.CurrentEnemy.Name == gm.CurrentPlayer.Name || gm.CurrentEnemy.Name == gm.CurrentEnemy.Name && playernum != gm.Players.Count - 1)
                {
                    playernum += 1;
                    gm.combat.ChangeEnemy(gm.Players[playernum]);
                }
            }
            if (playernum == gm.Players.Count -1)
            {
                CheckDead();
            }
        }
        /// <summary>
        /// Function runs through list and sorts list according to speed of every player
        /// </summary>
        public void ToChoosePlayer()
        {
            Debug.WriteLine("Choosing Player Turns");
            Debug.WriteLine("Turn Order: ");
            // Lambda function to iterate through the entire length of the list and prints the order of 
            //players in the debugger
            GameManager.Instance.Players.ForEach((x =>
                Debug.WriteLine(GameManager.Instance.Players.IndexOf(x) + " " + x.ToString())));
            Debug.WriteLine("Current Player is: " + GameManager.Instance.CurrentPlayer.Name);
            Debug.WriteLine("Current Enemy is: " + GameManager.Instance.CurrentEnemy.Name);
        }

        /// <summary>
        /// Simple function that checks if the last current player is dead
        /// </summary>
        /// <returns></returns>
        public bool CheckDead()
        {
            if (GameManager.Instance.CurrentPlayer.IsDead == true)
            {
                return true;
            }
            if (GameManager.Instance.CurrentEnemy.IsDead == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Function that goes to the IDLE state after the end of the turn process
        /// </summary>
        public void ToEndTurn()
        {
            GameManager.Instance.currentState = "IDLE";
            ToIdle();
            Debug.WriteLine("Ending Turn");
        }
    }
}