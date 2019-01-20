using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3110_Project_1
{
    class Map
    {

        public int Width { get; set; }
        public int Height { get; set; }
        public int EnemiesInRoom { get; set; }
        Tile[,] map;
        public Map(int x, int y)
        {
            Height = x;
            Width = y;

            map = new Tile[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || i == (x-1) || j == 0 || j == (y-1))
                    {
                        map[i, j] = new Tile('#');
                    }
                    else
                    {
                        map[i, j] = new Tile('.');
                    }
                }
            }
        }

        public void PrintMap()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j].Value + " ");
                }

                Console.WriteLine();
            }
        }

        public void Replace(int x, int y, char c)
        {
            map[x, y] = new Tile(c);
        }

        public Tile getTileAt(int x, int y)
        {
            return map[x, y];
        }

    }
}
