using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSimluator
{
    public partial class Form1 : Form
    {
        private bool IsFinished;//Determines whether the battle is finished or not 

        private Clan Clan1;
        private List<Warrior> Clan1_Team;

        private Clan Clan2;
        private List<Warrior> Clan2_Team;
        public Form1()
        {
            InitializeComponent();

            IsFinished = false;

            Clan1_Team = new List<Warrior>();
            Clan2_Team = new List<Warrior>();
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (!IsFinished)
            {
                status_txtb.Text = "----START----\r\n\r\n";
                Clan1_Team = CreateTeam(clan1_strength_trcb.Value, clan1_speed_trcb.Value, clan1_resistance_trcb.Value, clan1_length_trcb.Value);
                Clan1 = new Clan(clan1_name_txtb.Text, Clan1_Team);
                status_txtb.Text += $"{Clan1.Name} was created. Length: {Clan1.Warriors.Count}\r\n";

                Clan2_Team = CreateTeam(clan2_strength_trcb.Value, clan2_speed_trcb.Value, clan2_resistance_trcb.Value, clan2_length_trcb.Value);
                Clan2 = new Clan(clan2_name_txtb.Text, Clan2_Team);
                status_txtb.Text += $"{Clan2.Name} was created. Length: {Clan2.Warriors.Count}\r\n\r\n";
                //Create the teams

                status_txtb.Text += "Battle started";
                Tuple<double, double> percentageOfLoss = Battle(Clan1, Clan2);
                status_txtb.Text += $"Battle results:\r\n{Clan1.Name}: {(percentageOfLoss.Item1 * 100).ToString("#.##")}% loss\r\n{Clan2.Name}: {(percentageOfLoss.Item2 * 100).ToString("#.##")}% loss\r\n\r\n";
                //Battle

                Clan1.GetBattleResults(percentageOfLoss.Item1);
                Clan2.GetBattleResults(percentageOfLoss.Item2);
                status_txtb.Text += $"{Clan1.Name} has now {Clan1.Warriors.Count} warriors\r\n{Clan2.Name} has now {Clan2.Warriors.Count} warriors\r\n\r\n";
                //Get all the battle results

                status_txtb.Text += "----END----";
                IsFinished = true;//End of the battle

                start_btn.Text = "RESTART";
            }
            else
            {
                Restart();
                IsFinished = false;

                status_txtb.Text = "";
                start_btn.Text = "START";
            }
        }

        private void Restart()
        {
            clan1_name_txtb.Text = "";
            clan1_strength_trcb.Value = 0;
            clan1_speed_trcb.Value = 0;
            clan1_resistance_trcb.Value = 0;
            clan1_length_trcb.Value = 0;

            clan2_name_txtb.Text = "";
            clan2_strength_trcb.Value = 0;
            clan2_speed_trcb.Value = 0;
            clan2_resistance_trcb.Value = 0;
            clan2_length_trcb.Value = 0;

            Clan1 = null;
            Clan1_Team.Clear();

            Clan2 = null;
            Clan2_Team.Clear();
        }

        private static List<Warrior> CreateTeam(int strength, int speed, int resistance, int teamLength)
        {
            List<Warrior> team = new List<Warrior>(); 

            for(int i = 0; i < teamLength; i++)
            {
                team.Add(new Warrior(new Random().Next(0, strength), new Random().Next(0, speed), new Random().Next(0, resistance)));//Create a new warrior and assign a random value
                                                                                                                                           //greater or equal to 1 and less or equal to the 
                                                                                                                                           //value picked for the hability
            }

            return team;
        }//Create the warriors of the teams

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private static double GetPower(Clan clan)
        {
            double totalPower = 0.0f;

            foreach(Warrior w in clan.Warriors)
            {
                totalPower += w.Strength + w.Speed + w.Resistance;
            }

            if (totalPower > 0)
                return totalPower;
            else
                return 0.0f;
        }

        private static Tuple<double, double> Battle(Clan c1, Clan c2)
        {
            double c1_power = GetPower(c1);
            double c2_power = GetPower(c2);

            double total_clans_power = 0.0f;

            if(c1_power == 0.0f && c2_power == 0.0f)
                total_clans_power = 0.0f;
            else
                total_clans_power = c1_power + c2_power;

            double c1_loss = c1_power / total_clans_power;
            double c2_loss = c2_power / total_clans_power;
            //Get the percentage of the team that the clans lost on the battle

            if (total_clans_power > 0.0f)
                return new Tuple<double, double>(c1_loss, c2_loss);
            else
                return new Tuple<double, double>(0.00f, 0.00f);
        }
    }
}
