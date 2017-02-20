using System;
using System.Diagnostics;

namespace FSMAssessment
{
    class Combat
    {
        //sets turn manager variables
        private int turntoken = 0;
        public string combatLog;        

        public Combat()
        {
            //Constructor
        }

        public void ChangePlayer(Player p)
        {
            GameManager.Instance.CurrentPlayer = p;
        }

        public void ChangeEnemy(Player e)
        {
            GameManager.Instance.CurrentEnemy = e;
        }

        public void ToEnter()
        {
            GameManager gm = GameManager.Instance;
            //enters the attack function depending on which enemy is active
            Debug.WriteLine("Entering Attack...");
            if (!gm.CurrentEnemy.IsDead && !gm.CurrentPlayer.IsDead)
                ToAttack(gm.CurrentPlayer, gm.CurrentEnemy);
            else
                return;
        }

        public void PassAttack(Player player, Player target)
        {
            //Runs just the enemy's attack 
            target.Attack(player);
            turntoken = 0;
            combatLog += target.Name + " has attacked " + player.Name + " for " + (target.crit + target.Power).ToString() + " damage \n";
            Debug.WriteLine("Attacked");
            if (player.IsDead)
            {
                ToDeath(player, target);
                return;
            }
            ToExit();
        }

        public void ToAttack(Player current, Player target)
        {
            //checks to make sure current player and target enemy isn't dead
            if (current.Health != 0 && target.Health != 0)
            {
                current.Attack(target);
                combatLog += current.Name + " has attacked " + target.Name + " for " + (current.crit + current.Power).ToString() + " damage \n";
                turntoken += 1;
            }

            //runs the enemy's turn to attack
            if (turntoken >= 1)
            {
                target.Attack(current);
                turntoken = 0;
                combatLog += target.Name + " has attacked " + current.Name + " for " + (target.crit + target.Power).ToString() + " damage \n";
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

        public void ToDeath(Player current,Player target)
        {
            Debug.WriteLine("A player is Dead");
            combatLog += current.Name + " is dead \n";
            //Goes to function to leave the combat state
            target.Lvl++;

            target.Lvling();
            ToExit();
        }

        public void ToExit()
        {
            //simply states that the combat state is over
            combatLog += "End of combat turn... \n";
            combatLog += "-------------------------- \n";
            Debug.WriteLine("End of Combat");
        }
    }
}