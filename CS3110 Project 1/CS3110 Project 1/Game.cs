using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3110_Project_1
{
    class Game
    {
        public static void Battle(ref Map room, ref Player player, ref Enemy enemy)
        {
            bool playerTurn = true;
            ConsoleKeyInfo key;

            room.PrintMap();
            Console.WriteLine("Player Health: {0}/{1,-8} Enemy Health: {2}\nGrenades: {3}\n", player.HealthPoints, player.MaxHealth, enemy.HealthPoints, player.Grenades);

            while(player.HealthPoints > 0 && enemy.HealthPoints > 0 )
            {
                if (playerTurn)
                {
                    Console.Write("\nSpace to shoot, G to throw grenade, Esc to leave combat");
                    do
                    {
                        key = Console.ReadKey(true);
                    } while (!(key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.G || key.Key == ConsoleKey.Escape));

                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    else if(key.Key == ConsoleKey.Spacebar)
                    {
                        enemy.HealthPoints -= 2 + Math.Max(player.AttackEnemy() - enemy.Defense, 0);
                        if (enemy.HealthPoints <= 0)
                        {
                            enemy.HealthPoints = 0;
                        }

                        room.PrintMap();
                        Console.WriteLine("Player Health: {0}/{1,-8} Enemy Health: {2}\nGrenades: {3}\n", player.HealthPoints, player.MaxHealth, enemy.HealthPoints, player.Grenades);
                        Console.ReadKey(true);
                    }
                    else if(player.Grenades > 0)
                    {
                        enemy.HealthPoints -= 2 * player.AttackEnemy() - enemy.Defense;
                        if (enemy.HealthPoints < 0)
                        {
                            enemy.HealthPoints = 0;
                        }
                        player.Grenades--;

                        room.PrintMap();
                        Console.WriteLine("Player Health: {0}/{1,-8} Enemy Health: {2}\nGrenades: {3}\n", player.HealthPoints, player.MaxHealth, enemy.HealthPoints, player.Grenades);
                        Console.ReadKey(true);
                    }
                    else
                    {
                        room.PrintMap();
                        Console.WriteLine("Player Health: {0}/{1,-8} Enemy Health: {2}\nGrenades: {3}\n", player.HealthPoints, player.MaxHealth, enemy.HealthPoints, player.Grenades);
                        Console.WriteLine("No grenades!");
                        Console.ReadKey(true);
                    }

                    playerTurn = false;
                }
                else
                {
                    player.HealthPoints -= 1 + Math.Max(enemy.AttackPlayer() - player.Defense, 0);
                    if (player.HealthPoints <= 0)
                    {
                        player.HealthPoints = 0;
                        player.Defeated = true;
                        break;
                    }

                    room.PrintMap();
                    Console.WriteLine("Player Health: {0}/{1,-8} Enemy Health: {2}\nGrenades: {3}\n", player.HealthPoints, player.MaxHealth, enemy.HealthPoints, player.Grenades);
                    Console.Write("Enemy fired back!");
                    Console.ReadKey(true);

                    playerTurn = true;
                }
            }

            if (enemy.HealthPoints <= 0)
            {
                enemy.Defeated = true;
                player.AddExperience(enemy.Level);
                enemy.isDefeated(enemy.Defeated);
                room.Replace(enemy.LocationX, enemy.LocationY, '.');
                room.EnemiesInRoom--;
            }
        }

        static void Main(string[] args)
        {
            Map[] rooms = new Map[15];
            int currentRoomNumber = 1;
            Player player = new Player(6, 1);
            Enemy enemy;
            int enemyX = 0;
            int enemyY = 0;
            ConsoleKeyInfo key;
            bool EnemyInRange;

            //create maps and place room elements
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i] = new Map(12, 12);

                if(i == 1 || i == 4 || i == 7 || i == 9)
                {
                    rooms[i].Replace(5, 11, '>');
                    rooms[i].Replace(6, 11, '>');
                    rooms[i].Replace(9, 8, 'E');
                    rooms[i].EnemiesInRoom++;
                    rooms[i].Replace(4, 10, '+');
                }
                if(i == 4 || i == 7 || i == 10 || i == 12)
                {
                    rooms[i].Replace(5, 0, '<');
                    rooms[i].Replace(6, 0, '<');
                    if (i != 12)
                    {
                        rooms[i].Replace(3, 4, 'E');
                    }
                    else
                    {
                        rooms[i].Replace(6, 10, 'E');
                    }
                    rooms[i].EnemiesInRoom++;
                    if (!(i == 7 || i == 10))
                    {
                        rooms[i].Replace(6, 6, 'A');
                    }
                    rooms[i].Replace(6, 7, 'g');
                }
                if(i == 4 || i == 10 || i == 11)
                {
                    rooms[i].Replace(0, 5, '^');
                    rooms[i].Replace(0, 6, '^');
                    rooms[i].Replace(2, 7, 'E');
                    rooms[i].EnemiesInRoom++;
                }
                if(i == 3 || i == 9 || i == 10)
                {
                    rooms[i].Replace(11, 5, 'v');
                    rooms[i].Replace(11, 6, 'v');
                    rooms[i].Replace(10, 4, '+');
                    rooms[i].Replace(8, 3, 'E');
                    rooms[i].EnemiesInRoom++;
                }
            }

            Console.Write("You are Special Agent Jefferson Steelflex.\n\nThe hidden base of the terrorist group \"WalMart\" has been discovered "
                + "in the\nAustralian Outback.\n\nYou have been armed with a cutting-edge firearm: S&W NA-3. This weapon requires no "
                + "ammunition but at the cost of relatively short range.\n\nLuckily, we know the enemy has only short range weapons, as "
                + "well.\n\nIt is your duty to eradicate these low-lifes that threaten to destroy small\nbusinesses around the world."
                + "\n\nAre you ready to save the world, Agent Steelflex?\n\nPress any key to save the world.");

            Console.ReadKey(true);
 
            rooms[currentRoomNumber].Replace(player.LocationX, player.LocationY, '@');
            rooms[currentRoomNumber].PrintMap();
            Console.WriteLine("\nHealth: {0}/{1}\nAttack: {2}\nDefense: {3}\nRange: {4}\nLevel: {5}\nExperience: {6}/{7}\nGrenades: {8}\n\nPress Esc to quit."
                , player.HealthPoints, player.MaxHealth, player.Attack, player.Defense, player.Range, player.Level
                , player.Experience, player.ExperienceNeeded, player.Grenades);

            while (true)
            {
                player.RightMap = player.LeftMap = player.UpMap = player.DownMap = false;
                EnemyInRange = false;
                key = Console.ReadKey(true);

                if(key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                //check for enemies in range
                for (int i = player.LocationY; i <= Math.Min(player.LocationY + player.Range, rooms[currentRoomNumber].Width - 1); i++)
                {
                    if(rooms[currentRoomNumber].getTileAt(player.LocationX, i).Enemy)
                    {
                        EnemyInRange = true;
                        enemyX = player.LocationX;
                        enemyY = i;
                    }
                }
                for (int i = player.LocationY; i >= Math.Max(player.LocationY - player.Range, 0); i--)
                {
                    if (rooms[currentRoomNumber].getTileAt(player.LocationX, i).Enemy)
                    {
                        EnemyInRange = true;
                        enemyX = player.LocationX;
                        enemyY = i;
                    }
                }
                for (int i = player.LocationX; i <= Math.Min(player.LocationX + player.Range, rooms[currentRoomNumber].Height - 1); i++)
                {
                    if (rooms[currentRoomNumber].getTileAt(i, player.LocationY).Enemy)
                    {
                        EnemyInRange = true;
                        enemyX = i;
                        enemyY = player.LocationY;
                    }
                }
                for (int i = player.LocationX; i >= Math.Max(player.LocationX - player.Range, 0); i--)
                {
                    if (rooms[currentRoomNumber].getTileAt(i, player.LocationY).Enemy)
                    {
                        EnemyInRange = true;
                        enemyX = i;
                        enemyY = player.LocationY;
                    }
                } //end check for enemies

                if (EnemyInRange)
                {
                    enemy = new Enemy(enemyX, enemyY, player.Level);
                    Console.WriteLine("Enemy in range! Press space to engage!");
                    key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar && !player.Defeated)
                    {
                        Battle(ref rooms[currentRoomNumber], ref player, ref enemy);
                        if(player.Defeated)
                        {
                            break;
                        }
                    }
                    else if(key.Key == ConsoleKey.W || key.Key == ConsoleKey.A || key.Key == ConsoleKey.S || key.Key == ConsoleKey.D)
                    {
                        player.HealthPoints -= 1 + Math.Max(enemy.AttackPlayer() - player.Defense, 0);
                        Console.WriteLine("Enemy got a free shot at you!");
                        Console.ReadKey(true);
                    }
                }

                if (player.HealthPoints <= 0 || player.Defeated)
                {
                    player.HealthPoints = 0;
                    player.Defeated = true;
                    break;
                }
                if (currentRoomNumber == 12 && rooms[currentRoomNumber].EnemiesInRoom == 0)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("\n\n\n\n\nGreat work, Agent Steelflex!\nYou saved the world!!\n\n\n\n\n");
                    break;
                }

                player.MovePlayer(ref rooms[currentRoomNumber], key);

                //Check to see if player moves to a new room
                if(player.RightMap)
                {
                    currentRoomNumber += 3;
                    rooms[currentRoomNumber].Replace(player.LocationX, player.LocationY, '@');
                }
                if (player.LeftMap)
                {
                    currentRoomNumber -= 3;
                    rooms[currentRoomNumber].Replace(player.LocationX, player.LocationY, '@');
                }
                if (player.UpMap)
                {
                    currentRoomNumber--;
                    rooms[currentRoomNumber].Replace(player.LocationX, player.LocationY, '@');
                }
                if (player.DownMap)
                {
                    currentRoomNumber++;
                    rooms[currentRoomNumber].Replace(player.LocationX, player.LocationY, '@');
                }

                rooms[currentRoomNumber].PrintMap();
                Console.WriteLine("\nHealth: {0}/{1}\nAttack: {2}\nDefense: {3}\nRange: {4}\nLevel: {5}\nExperience: {6}/{7}\nGrenades: {8}\n\nPress Esc to quit."
                , player.HealthPoints, player.MaxHealth, player.Attack, player.Defense, player.Range, player.Level
                , player.Experience, player.ExperienceNeeded, player.Grenades);
            }

            if (player.Defeated)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("\n\n\n\n\nGAME OVER!\nThe world has ended!!\n\n\n\n\n");
            }
        }
    }
}
