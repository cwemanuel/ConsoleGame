using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3110_Project_1
{
    class Battle
    {
        public static void battle(ref Map room, ref Player player, ref Enemy enemy)
        {
            bool playerTurn = true;
            ConsoleKeyInfo key;

            room.PrintMap();
            Console.WriteLine("Player Health: {0}/{1,-8} Enemy Health: {2}\nGrenades: {3}\n", player.HealthPoints, player.MaxHealth, enemy.HealthPoints, player.Grenades);

            while (player.HealthPoints > 0 && enemy.HealthPoints > 0)
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
                    else if (key.Key == ConsoleKey.Spacebar)
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
                    else if (player.Grenades > 0)
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
    }
}
