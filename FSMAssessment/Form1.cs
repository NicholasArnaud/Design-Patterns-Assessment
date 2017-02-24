using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace FSMAssessment
{
    public partial class Form1 : Form
    {
        GameManager gm = GameManager.Instance; //Shortens code a little bit
        int potionlimit = 0;//Number of potions used..Max is 3

        public Form1()
        {
            InitializeComponent();
            SetUpForm();
        }

        void SetUpForm()
        {
            gm.Players.Sort((emp1, emp2) => emp1.Speed.CompareTo(emp2.Speed));
            gm.Players.Reverse();
            gm.turnManager.ToStartUp();
            DataManager<List<Player>>.Serialize("ListofPlayersDefualt", GameManager.Instance.Players);
            UpdateUI();
        }

        /// <summary>
        /// Default loading function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("Starting Up files...");
            StateFunctions();
            gm.GenFSM.TryTransition("IDLE");
        }

        /// <summary>
        /// Function that runs a case statement to run a function depending on state
        /// </summary>
        private void StateFunctions()
        {
            switch (gm.GenFSM.CurrentState.ToUpper())
            {
                case "INIT":
                    HelpText();
                    gm.turnManager.ToStartUp();
                    gm.GenFSM.CurrentState = "idle";
                    break;
                case "IDLE":
                    gm.turnManager.ToIdle();
                    gm.GenFSM.CurrentState = "turn";
                    break;
                case "TURN":
                    gm.turnManager.ToChoosePlayer();
                    gm.GenFSM.CurrentState = "atk";
                    break;
                case "ATK":
                    gm.turnManager.ToCombat();
                    gm.GenFSM.CurrentState = "endturn";
                    break;
                case "ENDTURN":
                    gm.turnManager.ToEndTurn();
                    gm.GenFSM.CurrentState = "idle";
                    break;
            }
            TextLog.SelectionStart = TextLog.Text.Length;
            TextLog.ScrollToCaret();
        }

        /// <summary>
        /// Updates various visual information about the players
        /// </summary>
        public void UpdateUI()
        {
            //Update Names
            EnemyName.Text = GameManager.Instance.CurrentEnemy.Name;
            PlayerName.Text = GameManager.Instance.CurrentPlayer.Name;
            //Update Health
            PlayerHealth.Value = GameManager.Instance.CurrentPlayer.Health;
            EnemyHealth.Value = GameManager.Instance.CurrentEnemy.Health;
            //Update Lvl
            Playerlvl.Text = "LVL: " + GameManager.Instance.CurrentPlayer.Lvl.ToString();
            Enemylvl.Text = "LVL: " + GameManager.Instance.CurrentEnemy.Lvl.ToString();

        }

        /// <summary>
        /// Enables: Atk, Potion, and PassTurn
        /// </summary>
        public void EnableButtons()
        {
            AtkButton.Enabled = true;
            PassTurn.Enabled = true;
            if (potionlimit <= 2)
                PotionButton.Enabled = true;
        }

        //Text Logs

        /// <summary>
        /// Displays about the game
        /// </summary>
        public void HelpText()
        {
            MessageBox.Show("Welcome to Brightest Dungeon!\n" +
            "This is much simplier than Darkest Dungeon...\n\n" +
            "HOW TO PLAY:\n" +
            "-You are the player on the left and your enemy is on the right.\n" +
            "-To Attack your enemy, just press the attack button in the center.\n" +
            "-You can also choose to pass your turn and not attack.\n" +
            "-To heal your currrent player, just press the potion button to heal.\n" +
            "-To save or load the game, you can press the two bottom buttons on the far left and right.\n" +
            "-You can also restart your current game by pressing the reset button.\n");
        }

        /// <summary>
        /// Runs a check and if all players or enemies are dead,
        /// will end the game
        /// </summary>
        public void Endlog()
        {
            if (GameManager.Instance.turnManager.CheckWin() == true)
            {
                AtkButton.Enabled = false;
                PotionButton.Enabled = false;
                PassTurn.Enabled = false;
                if (GameManager.Instance.CurrentPlayer.IsDead)
                {
                    UpdateLog("Mission Failed. We'll get'em next time...");
                    UpdateLog("Load a previous save or start over by pressing reset");
                }
                if (GameManager.Instance.CurrentEnemy.IsDead)
                {
                    UpdateLog("Congratulations!");
                    UpdateLog("You defeated the Brightest Dungeon! Please play again.");
                }
            }
            TextLog.SelectionStart = TextLog.Text.Length;
            TextLog.ScrollToCaret();
        }

        /// <summary>
        /// Enables the ability to add a message on the rich text document anywhere in the project
        /// </summary>
        /// <param name="message"></param>
        public void UpdateLog(string message)
        {
            TextLog.AppendText("\n" + message);
        }

        //Button Uses

        /// <summary>
        /// Restores a player's health a limited amount of times
        /// </summary>
        /// <param name="player"></param>
        public void PotionUse(Player player)
        {
            potionlimit += 1;
            if (potionlimit < 3)
            {
                player.Heal(100 - player.Health);
                UpdateUI();
            }
            else
                PotionButton.Enabled = false;
            TextLog.AppendText("Player has healed and now has used " + (potionlimit) + " potions. \n");
        }

        /// <summary>
        /// Runs attack function when pressed and continues the process of the fsm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AtkButton_Click(object sender, EventArgs e)
        {

            gm.GenFSM.TryTransition("TURN");
            StateFunctions();

            Debug.WriteLine("Started Combat state...");
            gm.GenFSM.TryTransition("ATK");
            StateFunctions();

            gm.GenFSM.TryTransition("ENDTURN");
            StateFunctions();
            UpdateUI();
            TextLog.Text = gm.Combat.combatLog;
            Endlog();
        }

        /// <summary>
        /// Player skips his attack and the enemy attacks the player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassTurn_Click(object sender, EventArgs e)
        {
            if (!gm.CurrentEnemy.IsDead)
                gm.Combat.PassAttack(gm.CurrentPlayer, gm.CurrentEnemy);
            else
                gm.Combat.PassAttack(gm.CurrentPlayer, gm.CurrentEnemy);
            gm.GenFSM.TryTransition("ENDTURN");
            StateFunctions();

            UpdateUI();
            TextLog.Text = gm.Combat.combatLog;
            Endlog();
        }

        /// <summary>
        /// Function that gives user full health and keeps track of how many are left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Potion_Click(object sender, EventArgs e)
        {
            PotionUse(GameManager.Instance.CurrentPlayer);
            UpdateUI();
        }

        //Other buttons

        /// <summary>
        /// Loads data from saved xml document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Loading previous save...");
            //Reloads how many potions have been used and reallows the ability to attack
            potionlimit = DataManager<int>.Deserialize("PotionUse");
            gm.CurrentPlayer = DataManager<Player>.Deserialize("CurrentPlayer");
            gm.CurrentEnemy = DataManager<Player>.Deserialize("CurrentEnemy");
            EnableButtons();
            UpdateUI();


            TextLog.AppendText("Previous Save Loaded... \n");
            Debug.WriteLine("Previous Save Loaded");
            TextLog.SelectionStart = TextLog.Text.Length;
            TextLog.ScrollToCaret();
        }

        /// <summary>
        /// Saves needed data into an xml format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            //Saves information on an xml file to be read later to be loaded
            Debug.WriteLine("Saving current progress...");
            DataManager<Player>.Serialize("CurrentPlayer", gm.CurrentPlayer);
            DataManager<Player>.Serialize("CurrentEnemy", gm.CurrentEnemy);

            DataManager<int>.Serialize("PotionUse", potionlimit);
            DataManager<string>.Serialize("TextLog", TextLog.Text);
            TextLog.SelectionStart = TextLog.Text.Length;
            TextLog.ScrollToCaret();
        }

        /// <summary>
        /// Resets needed data as if starting a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, EventArgs e)
        {
            //Resets data

            gm.Players = DataManager<List<Player>>.Deserialize("ListofPlayersDefualt");
            DataManager<string>.Serialize("Textlog", TextLog.Text = "");
            DataManager<int>.Serialize("PotionUse", potionlimit = 0);

            //Loads the reseted data
            gm.CurrentPlayer = gm.Players[0];
            gm.CurrentEnemy = gm.Players[1];
            
            UpdateUI();

            potionlimit = DataManager<int>.Deserialize("PotionUse");
            TextLog.Text = DataManager<string>.Deserialize("TextLog");
            EnableButtons();
            TextLog.Text = "Data has been reset...\n";
            Debug.WriteLine("Data has reset...");
        }

        /// <summary>
        /// Redisplays how to play the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpButton_Click(object sender, EventArgs e)
        {
            HelpText();
        }
    }
}