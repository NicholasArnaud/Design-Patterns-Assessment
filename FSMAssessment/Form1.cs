using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace FSMAssessment
{
    public enum TurnStates
    {
        INIT = 1,
        IDLE = 2,
        TURN = 3,
        ATK = 4,
        ENDTURN = 5,
        EXIT = 9000,
    }
    public partial class Form1 : Form
    {
        int potionlimit = 0;
        private bool buttonWasClicked = false;
        public static Form1 _Form1;

        public Form1()
        {
            InitializeComponent();
            _Form1 = this;
            SetUpForm();
        }

        void SetUpForm()
        {
            GameManager.Instance.Players.Sort((emp1, emp2) => emp1.Speed.CompareTo(emp2.Speed));
            GameManager.Instance.Players.Reverse();
            GameManager.Instance.turnManager.ToStartUp();
            GameManager.Instance.currentState = GameManager.Instance.stateSystem.Start();
            UpdateHealthBar();
            SetMaxHealthBar();
            DataManager<List<Player>>.Serialize("ListofPlayersDefualt", GameManager.Instance.Players);
            UpdateNames();
            UpdateLvl();
            UpdateHealthBar();
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
            GameManager.Instance.stateSystem.ChangeState("IDLE");
        }

        /// <summary>
        /// Enables to sync the heath bars of players anywhere else in the project
        /// </summary>
        /// <param name="player"></param>
        /// <param name="healthbarname"></param>
        public void UpdateHealthBar()
        {
            PlayerHealth.Value = GameManager.Instance.CurrentPlayer.Health;
            EnemyHealth.Value = GameManager.Instance.CurrentEnemy.Health;
        }

        public void SetMaxHealthBar()
        {
            PlayerHealth.Maximum = GameManager.Instance.CurrentPlayer.MaxHealth;
            EnemyHealth.Maximum = GameManager.Instance.CurrentEnemy.MaxHealth;
        }

        public void UpdateLvl()
        {
            Playerlvl.Text = "LVL: " + GameManager.Instance.CurrentPlayer.Lvl.ToString();
            Enemylvl.Text = "LVL: " + GameManager.Instance.CurrentEnemy.Lvl.ToString();
        }

        public void UpdateNames()
        {
            EnemyName.Text = GameManager.Instance.CurrentEnemy.Name;
            PlayerName.Text = GameManager.Instance.CurrentPlayer.Name;
        }

        /// <summary>
        /// Certain function is called depending on current state
        /// </summary>
        private void StateFunctions()
        {
            GameManager gm = GameManager.Instance;
            switch (gm.currentState)
            {
                case "INIT":
                    HelpText();
                    gm.turnManager.ToStartUp();
                    break;
                case "IDLE":
                    gm.turnManager.ToIdle();
                    break;
                case "TURN":
                    gm.turnManager.ToChoosePlayer();
                    break;
                case "ATK":
                    gm.combat.ToEnter();
                    break;
                case "ENDTURN":
                    gm.turnManager.ToEndTurn();
                    break;
            }
            TextLog.SelectionStart = TextLog.Text.Length;
            TextLog.ScrollToCaret();
        }

        /// <summary>
        /// Displays about the game
        /// </summary>
        public void HelpText()
        {
            UpdateLog("Welcome to Brightest Dungeon!");
            UpdateLog("This is much simplier than Darkest Dungeon...");
            UpdateLog("HOW TO PLAY:");
            UpdateLog("-You are the player on the left and your enemy is on the right.");
            UpdateLog("-To Attack your enemy, just press the attack button in the center.");
            UpdateLog("-You can also choose to pass your turn and not attack.");
            UpdateLog("-To heal your currrent player, just press the potion button to heal");
            UpdateLog("-To save or load the game, you can press the two bottom buttons on the far left and right");
            UpdateLog("-You can also restart your current game by pressing the reset button.");
        }

        /// <summary>
        /// Runs a check and if all players or enemies are dead,
        /// will end the game
        /// </summary>
        public void Endlog()
        {
            if (GameManager.Instance.turnManager.CheckDead() == true)
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
        /// Restores a player's health a limited amount of times
        /// </summary>
        /// <param name="player"></param>
        public void PotionUse(Player player)
        {
            potionlimit += 1;
            if (potionlimit < 3)
            {
                player.Heal(player.MaxHealth - player.Health);
                UpdateHealthBar();
            }
            else
                PotionButton.Enabled = false;
            TextLog.AppendText("Player has healed and now has used " + (potionlimit) + " potions. \n");
        }

        /// <summary>
        /// Enables the ability to add a message on the rich text document anywhere in the project
        /// </summary>
        /// <param name="message"></param>
        public void UpdateLog(string message)
        {
            TextLog.AppendText("\n" + message);
        }

        /// <summary>
        /// Enables a button press if diabled and disables a button press 
        /// if enabled when called depending on string name given
        /// </summary>
        /// <param name="buttonname"></param>
        public void ButtonEnable(string buttonname)
        {
            if (buttonname == AtkButton.Name)
            {
                AtkButton.Enabled = !AtkButton.Enabled;
            }
            if (buttonname == PotionButton.Name)
            {
                PotionButton.Enabled = !PotionButton.Enabled;
            }
        }

        /// <summary>
        /// Changes if a button was clicked 
        /// Don't need atm 
        /// </summary>
        /// <returns></returns>
        public bool CheckEndButton()
        {
            if (buttonWasClicked == true)
            {
                buttonWasClicked = false;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Runs attack function when pressed and continues the process of the fsm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AtkButton_Click(object sender, EventArgs e)
        {
            GameManager gm = GameManager.Instance;

            gm.stateSystem.ChangeState("TURN");
            StateFunctions();

            Debug.WriteLine("Started Combat state...");
            gm.stateSystem.ChangeState("ATK");
            StateFunctions();
            gm.stateSystem.ChangeState("ENDTURN");
            StateFunctions();
            UpdateNames();
            UpdateLvl();
            UpdateHealthBar();
            TextLog.Text = gm.combat.combatLog;
            Endlog();
        }

        /// <summary>
        /// Player skips his attack and the enemy attacks the player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassTurn_Click(object sender, EventArgs e)
        {
            GameManager gm = GameManager.Instance;
            if (!gm.CurrentEnemy.IsDead)
                gm.combat.PassAttack(gm.CurrentPlayer, gm.CurrentEnemy);
            else
                gm.combat.PassAttack(gm.CurrentPlayer, gm.CurrentEnemy);
            gm.stateSystem.ChangeState("ENDTURN");
            StateFunctions();

            UpdateNames();
            UpdateHealthBar();
            TextLog.Text = gm.combat.combatLog;
            Endlog();
        }

        //Other buttons

        /// <summary>
        /// Loads data from saved xml document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton_Click(object sender, EventArgs e)
        {
            GameManager gm = GameManager.Instance;

            //Loads previously saved information assuming files have been already created
            Debug.WriteLine("Loading previous save...");
            // gm.Players.ForEach(x =>DataManager<Player>.Deserialize(x.Name));
            //   gm.Players = DataManager<List<Player>>.Deserialize("ListofPlayers");
            //Reloads how many potions have been used and reallows the ability to attack
            potionlimit = DataManager<int>.Deserialize("PotionUse");
            if (potionlimit <= 2) PotionButton.Enabled = true;

            gm.CurrentPlayer = DataManager<Player>.Deserialize("CurrentPlayer");
            gm.CurrentEnemy = DataManager<Player>.Deserialize("CurrentEnemy");
            SetMaxHealthBar();
            UpdateHealthBar();
            UpdateNames();
            UpdateLvl();
            AtkButton.Enabled = true;
            PassTurn.Enabled = true;

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
            GameManager gm = GameManager.Instance;

            //Saves information on an xml file to be read later to be loaded
            Debug.WriteLine("Saving current progress...");
            DataManager<Player>.Serialize("CurrentPlayer", gm.CurrentPlayer);
            DataManager<Player>.Serialize("CurrentEnemy", gm.CurrentEnemy);

            //Lambda version of serializing all the players
            //"x" represents a "token" player and goes through the entire list of players to serialize with the file name being
            // "x.name" which will be the players name and saves the player of the class 
            // gm.Players.ForEach(x => DataManager<Player>.Serialize(x.Name, x));

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
            GameManager gm = GameManager.Instance;

            //Resets data

            gm.Players = DataManager<List<Player>>.Deserialize("ListofPlayersDefualt");
            DataManager<string>.Serialize("Textlog", TextLog.Text = "");
            DataManager<int>.Serialize("PotionUse", potionlimit = 0);

            //Loads the reseted data

            gm.CurrentPlayer = gm.Players[0];
            gm.CurrentEnemy = gm.Players[1];


            SetMaxHealthBar();
            UpdateHealthBar();
            UpdateNames();
            UpdateLvl();

            potionlimit = DataManager<int>.Deserialize("PotionUse");
            TextLog.Text = DataManager<string>.Deserialize("TextLog");
            AtkButton.Enabled = true;
            PotionButton.Enabled = true;
            PassTurn.Enabled = true;
            TextLog.Text = "Data has been reset...";
            Debug.WriteLine("Data has reset...");
        }


        /// <summary>
        /// Function that gives user full health and keeps track of how many are left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Potion_Click(object sender, EventArgs e)
        {
            PotionUse(GameManager.Instance.CurrentPlayer);
            UpdateHealthBar();
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