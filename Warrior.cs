using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSimluator
{
    class Warrior
    {
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Resistance { get; set; }

        public Warrior(int strength, int speed, int resistance)
        {
            Strength = strength;
            Speed = speed;
            Resistance = resistance;
        }
    }
}
