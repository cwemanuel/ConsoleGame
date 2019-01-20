using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3110_Project_1
{
    class Enemy
    {
        public bool Defeated { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public int HealthPoints { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int Level { get; set; }

        public Enemy(int x, int y, int level)
        {
            Defeated = false;
            LocationX = x;
            LocationY = y;
            Level = level;
            HealthPoints = Level * 10;
            Defense = Level * 10;
            Attack = Level * 10;
        }

        public int AttackPlayer()
        {
            int damage = Attack;
            return damage;
        }

        public void isDefeated(bool Defeated)
        {
            if (Defeated == true)
            {
                Attack = Defense = Level = HealthPoints = 0;
            }
        }


    }
}
