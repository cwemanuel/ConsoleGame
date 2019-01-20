using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS3110_Project_1
{
    class Tile
    {
        public char Value { get; set; }
        public bool Passable { get; set; }
        public bool Door { get; set; }
        public bool Enemy { get; set; }
        public Enemy Enemy1 { get; set; }
        public Enemy Enemy2 { get; set; }

        public Tile(char c)
        {
            Value = c;

            if (c.Equals('.') || c.Equals('A') || c.Equals('+') || c.Equals('g'))
            {
                Passable = true;
            }
            else if (c.Equals('E') || c.Equals('B'))
            {
                Enemy = true;
            }
            else if (c.Equals('^') || c.Equals('>') || c.Equals('v') || c.Equals('<'))
            {
                Door = true;
            }
        }
    }
}
