using System;

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

    public class Player
    {
        GenFSM gmFSM = GameManager.Instance.GenFSM; //Shortens code a little bit

        public Player()
        { //Constructor
            gmFSM.States.Add(TurnStates.ENDTURN.ToString());
            gmFSM.States.Add(TurnStates.EXIT.ToString());
            gmFSM.States.Add(TurnStates.IDLE.ToString());
            gmFSM.States.Add(TurnStates.TURN.ToString());
            gmFSM.States.Add(TurnStates.ATK.ToString());
            gmFSM.AddTransitions("Init", "idle", false);
            gmFSM.AddTransitions("idle", "turn", false);
            gmFSM.AddTransitions("turn", "atk", false);
            gmFSM.AddTransitions("atk", "endturn", false);
            gmFSM.AddTransitions("endturn", "idle", false);
        }

        /// <summary>
        /// Creates Player with given values
        /// </summary>
        /// <param name="name">Sets player name</param>
        /// <param name="power">Sets player damage</param>
        /// <param name="speed">Sets player speed</param>
        /// <param name="critmax">Sets player max added damage</param>
        public Player(string name, int power, float speed, int critmax)
        {
            //sets all variables for the player on creation
            m_name = name;
            m_power = power;
            m_speed = speed;
            m_critMax = critmax;
            m_health = 100;
            m_lvl = 1;
            GameManager.Instance.Players.Add(this);
        }


        //getters and setters for variables outside the class for public use
        public int Health
        {
            get { return m_health; }
            set
            {
                if (value >= 0)
                    m_health = value;
                else m_health = 0;
            }
        }
        public int Power
        {
            get { return m_power; }
            set { m_power = value; }
        }
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public float Speed
        {
            get { return m_speed; }
            set { m_speed = value; }
        }
        public int CritMax
        {
            get { return m_critMax; }
            set { m_critMax = value; }
        }
        public bool IsDead
        {
            get { return m_isDead; }
            set { m_isDead = value; }
        }
        public int Lvl
        {
            get { return m_lvl; }
            set { m_lvl = value; }
        }

        /// <summary>
        /// When running the "ToString()" function.
        /// The function will return the name of a player that called it
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return m_name;
        }
        
        /// <summary>
        /// Allows a player to lose health when attacked
        /// </summary>
        /// <param name="amount">Total amount of damage taken from an enemy</param>
        public void TakeDamage(int amount)
        {
            m_health -= amount;
            if (m_health <= 0)
            {
                m_health = 0;
                IsDead = true;
            }

        }

        /// <summary>
        /// Runs when the "Potion" button is pressed
        /// Heals the current player to full health
        /// </summary>
        /// <param name="amount">Amount taken from the max health subtracted with the current health</param>
        public void Heal(int amount)
        {
            m_health += amount;
        }

        /// <summary>
        /// Called from the attack fsm to do damage calculations
        /// </summary>
        /// <param name="target">The targeted player</param>
        public void Attack(Player target)
        {
            Random rnd = new Random();
            m_crit = rnd.Next(0, CritMax);
            int damage = m_power + m_crit;
            target.TakeDamage(damage);
        }

        /// <summary>
        /// Levels up a player that killed an enemy
        /// </summary>
        public void Lvling()
        {
            m_health += 100 - m_health;
            if (m_lvl % 2 == 0)
            {
                m_critMax += 4;
                Power += 1;
            }
        }


        private int m_lvl;
        public int m_crit;
        private int m_critMax;
        private bool m_isDead;
        private string m_name;
        private int m_health;
        private int m_power;
        private float m_speed;
    }
}