using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMAssessment
{
    public class Player
    {
        public Player()
        { //Constructor
        }

        public Player(string Name, int Health, int Power, float Speed, int CritMax)
        {
            //sets all variables for the player on creation
            m_name = Name;
            m_health = Health;
            m_power = Power;
            m_speed = Speed;
            m_maxHealth = m_health;
            m_critMax = CritMax;
            m_lvl = 1;
            GameManager.Instance.Players.Add(this);
        }


        //getters and setters for variables outside the class for public use
        public int MaxHealth
        {
            get { return m_maxHealth; }
            set { m_maxHealth = value; }
        }
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


        public override string ToString()
        {
            return m_name;
        }

        public void TakeDamage(int amount)
        {
            m_health -= amount;
            if (m_health <= 0)
            {
                m_health = 0;
                IsDead = true;
            }

        }

        public void Heal(int amount)
        {
            m_health += amount;
        }

        public void Attack(Player target)
        {
            Random rnd = new Random();
            crit = rnd.Next(0, CritMax);
            int damage = m_power + crit;
            target.TakeDamage(damage);
        }

        public void Lvling()
        {
            m_health = m_maxHealth;
            if (Lvl % 2 == 0)
            {
                CritMax += 4;
                Power += 1;
            }
        }


        private int m_lvl;
        public int crit;
        private int m_critMax;
        private bool m_isDead;
        private int m_maxHealth;
        private string m_name;
        private int m_health;
        private int m_power;
        private float m_speed;
    }
}