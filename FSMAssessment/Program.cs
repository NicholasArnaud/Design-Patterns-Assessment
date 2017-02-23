using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSMAssessment
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Creates everything used inside program
            GenFSM fsm = new GenFSM("INIT");
            GameManager.Instance.GenFSM = fsm;
            GameManager.Instance.Players = new List<Player>();
            TurnManager turnManager = new TurnManager();

            //creates all the needed entities on the start of the process
            //Note: all players are automatically added to the list of players inside their constructor
            Player Swine = new Player("Infested Swine", 5, 3.4f, 6);
            Player Doomsday = new Player("Doomsday", 6, 5.5f, 4);
            Player Aries = new Player("Aries", 2, 7.8f, 7);
            Player Jingles = new Player("Jester", 3, 10.3f, 5);
            Player Vyral = new Player("Vyral", 4, 6, 2);
            Player Syran = new Player("Syran", 5, 8.7f, 4);
            Player CurrentPlayer = new Player();
            Player CurrentEnemy = new Player();

            //sets information into singleton

            GameManager.Instance.CurrentPlayer = CurrentPlayer;
            GameManager.Instance.CurrentEnemy = CurrentEnemy;
            GameManager.Instance.turnManager = turnManager;


            Combat combat = new Combat();
            GameManager.Instance.combat = combat;


            //Default
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}