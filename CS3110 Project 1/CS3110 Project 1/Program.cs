using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3110_Project_1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] map = { {'#','#','#','#'},
                            {'#','.','.','#'},
                            {'#','r','E','#'},
                            {'#','#','#','#'} };

            Tile[,] tiles = new Tile[map.GetLength(0), map.GetLength(1)];

            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    tiles[i, j] = new Tile(map[i, j]);
                    Console.Write(map[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.Write("Enter coordinate of tile to check properties.\nRow: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Column: ");
            int y = Convert.ToInt32(Console.ReadLine());

            if(tiles[x,y].Passable)
            {
                Console.WriteLine("Passable: True");
            }
            else
            {
                Console.WriteLine("Passable: False");
            }
            if (tiles[x, y].Body)
            {
                Console.WriteLine("Player or enemy: True");
            }
            else
            {
                Console.WriteLine("Player or enemy: False");
            }
            if (tiles[x, y].Item)
            {
                Console.WriteLine("Item: True");
            }
            else
            {
                Console.WriteLine("Item: False");
            }

            Console.WriteLine();

            Player player = new Player();

            if(player.Spawned)
            {
                Replace(ref map, 1, 1, '@');
            }

            tiles[1, 1] = new Tile(map[1, 1]);

            Console.Clear();

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.Write("Enter coordinate of tile to check properties.\nRow: ");
            x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Column: ");
            y = Convert.ToInt32(Console.ReadLine());

            if (tiles[x, y].Passable)
            {
                Console.WriteLine("Passable: True");
            }
            else
            {
                Console.WriteLine("Passable: False");
            }
            if (tiles[x, y].Body)
            {
                Console.WriteLine("Player or enemy: True");
            }
            else
            {
                Console.WriteLine("Player or enemy: False");
            }
            if (tiles[x, y].Item)
            {
                Console.WriteLine("Item: True");
            }
            else
            {
                Console.WriteLine("Item: False");
            }
        }

        public static void Replace(ref char[,] map, int x, int y, char c)
        {
            map[x, y] = c;
        }
    }
}
