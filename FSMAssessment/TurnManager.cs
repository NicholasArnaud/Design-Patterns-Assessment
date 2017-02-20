
using System.Diagnostics;

namespace FSMAssessment
{
    public class TurnManager
    {
        int pCountCheck = 0;
        int playernum = 0;
        public TurnManager()
        {
            //Constructor
        }

        public void ToStartUp()
        {

            Debug.WriteLine("Starting Up");
            GameManager.Instance.currentState = "IDLE";
            GameManager.Instance.CurrentPlayer = GameManager.Instance.Players[0];
            GameManager.Instance.CurrentEnemy = GameManager.Instance.Players[1];
        }

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
                Form1._Form1.SetMaxHealthBar();
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
                Form1._Form1.SetMaxHealthBar();
            }
            if (playernum == gm.Players.Count -1)
            {
                CheckDead();
            }
        }

        public void ToChoosePlayer()
        {
            pCountCheck++;
            if (pCountCheck == GameManager.Instance.Players.Count) pCountCheck = 0;
            Debug.WriteLine("Choosing Player Turns");
            Debug.WriteLine("Turn Order: ");
            // Lambda function to iterate through the entire length of the list and prints the order of 
            //players in the debugger
            GameManager.Instance.Players.ForEach((x =>
                Debug.WriteLine(GameManager.Instance.Players.IndexOf(x) + " " + x.ToString())));
            Debug.WriteLine("Current Player is: " + GameManager.Instance.CurrentPlayer.Name);
            Debug.WriteLine("Current Enemy is: " + GameManager.Instance.CurrentEnemy.Name);
        }

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

        public void ToEndTurn()
        {
            GameManager.Instance.currentState = "IDLE";
            ToIdle();
            Form1._Form1.UpdateLog("\n" + "End of Turn" + "\n");
            Debug.WriteLine("Ending Turn");
        }
    }
}