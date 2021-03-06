﻿using System;
using System.Collections.Generic;
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
            GameManager gm = GameManager.Instance;

            //Creates everything used inside program
            GenFSM fsm = new GenFSM("INIT");
            gm.GenFSM = fsm;
            gm.Players = new List<Player>();
            TurnManager turnManager = new TurnManager();

            //creates all the needed entities on the start of the process
            //Note: all players are automatically added to the list of players inside their constructor
            Player Swine = new Player("Infested Swine", 6, 3.4f, 12);
            Player Doomsday = new Player("Doomsday", 7, 5.5f, 5);
            Player Aries = new Player("Aries", 2, 7.8f, 7);
            Player Jingles = new Player("Jester", 3, 10.3f, 9);
            Player Vyral = new Player("Vyral", 4, 6, 2);
            Player Syran = new Player("Syran", 6, 8.7f, 3);
            Player CurrentPlayer = new Player();
            Player CurrentEnemy = new Player();

            //sets information into singleton

            gm.CurrentPlayer = CurrentPlayer;
            gm.CurrentEnemy = CurrentEnemy;
            gm.turnManager = turnManager;


            Combat combat = new Combat();
            gm.Combat = combat;


            //Default
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}