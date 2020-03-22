using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSimluator
{
    class Clan
    {
        public List<Warrior> Warriors { get; private set; }
        public int ClanLength { get; set; }

        public string Name { get; private set; }

        public Clan(string name, List<Warrior> warriors)
        {
            Name = name;

            Warriors = warriors;

            ClanLength = warriors.Count;
        }

        private int Battle()
        {
            int clanPower = 0;

            foreach(Warrior w in Warriors)
            {
                clanPower += (3 * w.Strength) + (2 * w.Speed) + w.Resistance;
            }

            return clanPower;
        }

        public void GetBattleResults(double percentageOfDeaths)
        {
            List<Warrior> resultArmy = new List<Warrior>();
            int newArmyLength = 0;

            if (percentageOfDeaths > 0)
                newArmyLength = ClanLength - Convert.ToInt32(Convert.ToDouble(ClanLength) * percentageOfDeaths);//Get the amount of warriors the clan will have based on the percentage of the dead ones
                                                                                                   //Round the value to the smallest as possible

            if(newArmyLength > 0)
                for(int i = 0; i < newArmyLength; i++)
                {
                    resultArmy.Add(Warriors[i]);
                }//displace the dead soldiers
            //Displayed if the length of the new army is greater than 0

            Warriors = resultArmy;
        }

        private int GetTheSmallerInt(double v)
        {
            return Convert.ToInt32(v) - 1;
        }
    }
}
