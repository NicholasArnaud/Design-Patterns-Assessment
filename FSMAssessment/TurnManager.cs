using System.Diagnostics;

namespace FSMAssessment
{
    public class TurnManager
    {
        GameManager gm = GameManager.Instance; //Shortens code a bit

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
            gm.GenFSM.CurrentState = "IDLE";
            gm.CurrentPlayer = gm.Players[0];
            gm.CurrentEnemy = gm.Players[1];
        }

        /// <summary>
        /// Function checks between turns if any current player is dead or there are no more players left
        /// </summary>
        public void ToIdle()
        {
            Debug.WriteLine("Waiting...");
            if (gm.CurrentPlayer.IsDead && gm.Players.IndexOf(gm.CurrentPlayer)!= gm.Players.Count -1)
            {
                gm.CurrentPlayer = gm.Players[gm.Players.IndexOf(gm.CurrentPlayer)+1];
                if(gm.CurrentPlayer == gm.CurrentEnemy && gm.Players.IndexOf(gm.CurrentPlayer) != gm.Players.Count - 1)
                    gm.CurrentPlayer = gm.Players[gm.Players.IndexOf(gm.CurrentPlayer) + 1];
            }
            else if (gm.CurrentEnemy.IsDead && gm.Players.IndexOf(gm.CurrentEnemy) != gm.Players.Count -1)
            {
                gm.CurrentEnemy = gm.Players[gm.Players.IndexOf(gm.CurrentEnemy) + 1];
                if (gm.CurrentEnemy == gm.CurrentPlayer && gm.Players.IndexOf(gm.CurrentPlayer) != gm.Players.Count - 1)
                    gm.CurrentEnemy = gm.Players[gm.Players.IndexOf(gm.CurrentEnemy) + 1];
            }
            else
                CheckWin();
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
            gm.Players.ForEach((x =>Debug.WriteLine(gm.Players.IndexOf(x) + " " + x.ToString())));
                
            Debug.WriteLine("Current Player is: " +gm.CurrentPlayer.Name);
            Debug.WriteLine("Current Enemy is: " + gm.CurrentEnemy.Name);
        }

        /// <summary>
        /// Enters Combat FSM
        /// </summary>
        public void ToCombat()
        {
            gm.Combat.ToEnter();
        }

        /// <summary>
        /// Simple function that checks if the last current player is dead
        /// </summary>
        /// <returns></returns>
        public bool CheckWin()
        {
            if (gm.CurrentPlayer.IsDead == true)
            {
                return true;
            }
            if (gm.CurrentEnemy.IsDead == true)
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
            gm.GenFSM.CurrentState = "idle";
            ToIdle();
            Debug.WriteLine("Ending Turn");
        }
    }
}