using System.Diagnostics;

namespace FSMAssessment
{
    class Combat
    {
        GameManager gm = GameManager.Instance;
        private int turntoken = 0; //sets turn manager variables
        public string combatLog;        

        /// <summary>
        /// Tis but a constuctor
        /// </summary>
        public Combat()
        {
        }
        
        /// <summary>
        /// Takes in the player in the argument to set as the current player
        /// </summary>
        /// <param name="p"> Player to set as the current player</param>
        public void ChangePlayer(Player p)
        {
            gm.CurrentPlayer = p;
        }

        /// <summary>
        /// Takes in the enemy in the argument to set as the current player
        /// </summary>
        /// <param name="e">Player to set as the current enemy</param>
        public void ChangeEnemy(Player e)
        {
            gm.CurrentEnemy = e;
        }

        /// <summary>
        /// Checks to see if any player is dead then goes to the attack function with both 
        /// current players
        /// </summary>
        public void ToEnter()
        {
            
            //enters the attack function depending on which enemy is active
            Debug.WriteLine("Entering Attack...");
            if (!gm.CurrentEnemy.IsDead && !gm.CurrentPlayer.IsDead)
                ToAttack(gm.CurrentPlayer, gm.CurrentEnemy);
            else
                return;
        }

        /// <summary>
        /// Runs when player presses the "Pass Turn" button and the enemy attacks 
        /// the current player
        /// </summary>
        /// <param name="player">Current player</param>
        /// <param name="target">Current enemy</param>
        public void PassAttack(Player player, Player target)
        {
            //Runs just the enemy's attack 
            target.Attack(player);
            turntoken = 0;
            combatLog += target.Name + " has attacked " + player.Name + " for " + (target.m_crit + target.Power).ToString() + " damage \n";
            Debug.WriteLine("Attacked");
            if (player.IsDead)
            {
                ToDeath(player, target);
                return;
            }
            ToExit();
        }

        /// <summary>
        /// Checks and runs the attack functions for each the current player
        /// and current enemy while adding text for the combat log
        /// Goes to ToDeath function if a player died
        /// </summary>
        /// <param name="current">Current player</param>
        /// <param name="target">Current enemy</param>
        public void ToAttack(Player current, Player target)
        {
            //checks to make sure current player and target enemy isn't dead
            if (current.Health != 0 && target.Health != 0)
            {
                current.Attack(target);
                combatLog += current.Name + " has attacked " + target.Name + " for " + (current.m_crit + current.Power).ToString() + " damage \n";
                turntoken += 1;
            }

            //runs the enemy's turn to attack
            if (turntoken >= 1)
            {
                target.Attack(current);
                turntoken = 0;
                combatLog += target.Name + " has attacked " + current.Name + " for " + (target.m_crit + target.Power).ToString() + " damage \n";
            }
            Debug.WriteLine("Attacked");
            //runs death function if the current player is dead or the enemy is dead
            if (target.IsDead)
            {
                ToDeath(target, current);
                return;
            }
            else if (current.IsDead)
            {
                ToDeath(current, target);
                return;
            }
            //goes to the exit combat function if the current player isnt dead
            if (!current.IsDead)
                ToExit();
        }

        /// <summary>
        /// Prints to combat log who died and runs lvling function
        /// to level up the player who survived
        /// </summary>
        /// <param name="current">Dead player</param>
        /// <param name="target">Surviving player</param>
        public void ToDeath(Player current,Player target)
        {
            Debug.WriteLine("A player is Dead");
            combatLog += current.Name + " is dead \n";
            //Goes to function to leave the combat state
            target.Lvl++;

            target.Lvling();
            ToExit();
        }

        /// <summary>
        /// Leaves the combat class
        /// </summary>
        public void ToExit()
        {
            //simply states that the combat state is over
            combatLog += "End of combat turn... \n";
            combatLog += "-------------------------- \n";
            Debug.WriteLine("End of Combat");
        }
    }
}