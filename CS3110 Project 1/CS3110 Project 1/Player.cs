using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3110_Project_1
{
    class Player
    {
        public bool Spawned { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public bool UpMap { get; set; }
        public bool RightMap { get; set; }
        public bool DownMap { get; set; }
        public bool LeftMap { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceNeeded { get; set; }
        public int HealthPoints { get; set; }
        public int MaxHealth { get; set; }
        public int Defense { get; set; }
        public int Attack { get; set; }
        public int Range { get; set; }
        public bool Defeated { get; set; }
        public int Grenades { get; set; }

        public Player()
        {
            Spawned = true;
            LocationX = 6;
            LocationY = 1;
        }

        public Player(int x, int y)
        {
            Spawned = true;
            LocationX = x;
            LocationY = y;
            Level = 1;
            Attack = 10;
            Defense = 10;
            HealthPoints = 10;
            MaxHealth = HealthPoints;
            Experience = 0;
            ExperienceNeeded = 20;
            Range = 3;
            Grenades = 0;
            Defeated = false;
        }

        public void MovePlayer(ref Map map, ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.D)
            {
                if (map.getTileAt(LocationX, LocationY + 1).Door && map.EnemiesInRoom == 0)
                {
                    RightMap = true;
                    map.Replace(LocationX, LocationY, '.');
                    LocationY = 1;
                }
                else if (map.getTileAt(LocationX, LocationY + 1).Passable)
                {
                    if(map.getTileAt(LocationX, LocationY + 1).Value == '+')
                    {
                        HealthPoints += 5;
                        if(HealthPoints > MaxHealth)
                        {
                            HealthPoints = MaxHealth;
                        }
                        Console.Write("Picked up a First-Aid Kit!");
                        Console.ReadKey(true);
                    }
                    if(map.getTileAt(LocationX, LocationY + 1).Value == 'A')
                    {
                        Defense += 5;
                        Console.Write("Picked up armor!");
                        Console.ReadKey(true);
                    }
                    if (map.getTileAt(LocationX, LocationY + 1).Value == 'g')
                    {
                        Grenades++;
                        Console.Write("Picked up a grenade!");
                        Console.ReadKey(true);
                    }
                    LocationY++;
                    map.Replace(LocationX, LocationY, '@');
                    map.Replace(LocationX, LocationY - 1, '.');
                }
            }
            if (key.Key == ConsoleKey.A)
            {
                if (map.getTileAt(LocationX, LocationY - 1).Door && map.EnemiesInRoom == 0)
                {
                    LeftMap = true;
                    map.Replace(LocationX, LocationY, '.');
                    LocationY = 10;
                }
                else if (map.getTileAt(LocationX, LocationY - 1).Passable)
                {
                    if(map.getTileAt(LocationX, LocationY - 1).Value == '+')
                    {
                        HealthPoints += 5;
                        if (HealthPoints > MaxHealth)
                        {
                            HealthPoints = MaxHealth;
                        }
                        Console.Write("Picked up a First-Aid Kit!");
                        Console.ReadKey(true);
                    }
                    if (map.getTileAt(LocationX, LocationY - 1).Value == 'A')
                    {
                        Defense += 5;
                        Console.Write("Picked up armor!");
                        Console.ReadKey(true);
                    }
                    if (map.getTileAt(LocationX, LocationY - 1).Value == 'g')
                    {
                        Grenades++;
                        Console.Write("Picked up a grenade!");
                        Console.ReadKey(true);
                    }
                    LocationY--;
                    map.Replace(LocationX, LocationY, '@');
                    map.Replace(LocationX, LocationY + 1, '.');
                }
            }
            if (key.Key == ConsoleKey.W)
            {
                if (map.getTileAt(LocationX - 1, LocationY).Door && map.EnemiesInRoom == 0)
                {
                    UpMap = true;
                    map.Replace(LocationX, LocationY, '.');
                    LocationX = 10;
                }
                else if (map.getTileAt(LocationX - 1, LocationY).Passable)
                {
                    if(map.getTileAt(LocationX - 1, LocationY).Value == '+')
                    {
                        HealthPoints += 5;
                        if (HealthPoints > MaxHealth)
                        {
                            HealthPoints = MaxHealth;
                        }
                        Console.Write("Picked up a First-Aid Kit!");
                        Console.ReadKey(true);
                    }
                    if (map.getTileAt(LocationX - 1, LocationY).Value == 'A')
                    {
                        Defense += 5;
                        Console.Write("Picked up armor!");
                        Console.ReadKey(true);
                    }
                    if (map.getTileAt(LocationX - 1, LocationY).Value == 'g')
                    {
                        Grenades++;
                        Console.Write("Picked up a grenade!");
                        Console.ReadKey(true);
                    }
                    LocationX--;
                    map.Replace(LocationX, LocationY, '@');
                    map.Replace(LocationX + 1, LocationY, '.');
                }
            }
            if (key.Key == ConsoleKey.S)
            {
                if (map.getTileAt(LocationX + 1, LocationY).Door && map.EnemiesInRoom == 0)
                {
                    DownMap = true;
                    map.Replace(LocationX, LocationY, '.');
                    LocationX = 1;
                }
                else if (map.getTileAt(LocationX + 1, LocationY).Passable)
                {
                    if(map.getTileAt(LocationX + 1, LocationY).Value == '+')
                    {
                        HealthPoints += 5;
                        if (HealthPoints > MaxHealth)
                        {
                            HealthPoints = MaxHealth;
                        }
                        Console.Write("Picked up a First-Aid Kit!");
                        Console.ReadKey(true);
                    }
                    if (map.getTileAt(LocationX + 1, LocationY).Value == 'A')
                    {
                        Defense += 5;
                        Console.Write("Picked up armor!");
                        Console.ReadKey(true);
                    }
                    if (map.getTileAt(LocationX + 1, LocationY).Value == 'g')
                    {
                        Grenades++;
                        Console.Write("Picked up a grenade!");
                        Console.ReadKey(true);
                    }
                    LocationX++;
                    map.Replace(LocationX, LocationY, '@');
                    map.Replace(LocationX - 1, LocationY, '.');
                }
            }
        }

        public int AttackEnemy()
        {
            int damage = Attack;
            return damage;
        }

        public void AddExperience(int levelDefeated)
        {
            Experience += levelDefeated * 10;
            Console.Write("\nYou got {0} experience!", levelDefeated * 10);
            Console.ReadKey(true);
            while( Experience >= ExperienceNeeded)
            {
                Console.Write("\nYou leveled up!");
                Console.ReadKey(true);
                Experience = Experience - ExperienceNeeded;                
                Level++;
                Attack += 4 * Level;
                Defense += 3 * Level;
                MaxHealth += 5 * Level;
                ExperienceNeeded = 6 * (Level * Level);
                HealthPoints = MaxHealth;
            }
            
        }
    }
}
