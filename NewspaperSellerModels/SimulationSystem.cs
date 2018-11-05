using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NewspaperSellerModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DayTypeDistributions = new List<DayTypeDistribution>();
            DemandDistributions = new List<DemandDistribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
        }
        ///////////// INPUTS /////////////
        public int NumOfNewspapers { get; set; }
        public int NumOfRecords { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal ScrapPrice { get; set; }
        public decimal UnitProfit { get; set; }
        public List<DayTypeDistribution> DayTypeDistributions { get; set; }
        public List<DemandDistribution> DemandDistributions { get; set; }

        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }

        public void start_simulation(string filepath)
        {
            ReadInput(filepath);
            generate_cumulative_range();
            generate_cumulative_Demand_range();
        }
        public void ReadInput(string filepath)
        {
            string str;
            FileStream fs = new FileStream(filepath, FileMode.Open);
            StreamReader SR = new StreamReader(fs);
            //    char s = (char)SR.Read();
            while (SR.Peek() != -1)
            {
                str = SR.ReadLine();
                if (str == "NumOfNewspapers")
                {
                    NumOfNewspapers = int.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "NumOfRecords")
                {
                    NumOfRecords = int.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "PurchasePrice")
                {
                    PurchasePrice = decimal.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "ScrapPrice")
                {
                    ScrapPrice = decimal.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "SellingPrice")
                {
                    SellingPrice = decimal.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "DayTypeDistributions")
                {
                    str = SR.ReadLine();
                    string[] substrings = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    DayTypeDistribution DTD_good = new DayTypeDistribution();
                    DayTypeDistribution DTD_fair = new DayTypeDistribution();
                    DayTypeDistribution DTD_poor = new DayTypeDistribution();
                    DTD_good.Probability = decimal.Parse(substrings[0]);
                    DTD_good.DayType = (Enums.DayType)0;
                    DTD_fair.Probability = decimal.Parse(substrings[1]);
                    DTD_fair.DayType = (Enums.DayType)1;
                    DTD_poor.Probability = decimal.Parse(substrings[2]);
                    DTD_poor.DayType = (Enums.DayType)2;                   
                    DayTypeDistributions.Add(DTD_good);
                    DayTypeDistributions.Add(DTD_fair);
                    DayTypeDistributions.Add(DTD_poor);
                    str = SR.ReadLine();
                    continue;
                }
                else
                {               
                        str = SR.ReadLine();
                        while (str != "" && str != null)
                        {
                            string[] substrings = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            DemandDistribution DD = new DemandDistribution();
                            DD.Demand = int.Parse(substrings[0]);
                            DayTypeDistribution DTD_good = new DayTypeDistribution();
                            DayTypeDistribution DTD_fair = new DayTypeDistribution();
                            DayTypeDistribution DTD_poor = new DayTypeDistribution();
                            DTD_good.Probability = decimal.Parse(substrings[1]);
                            DTD_good.DayType = Enums.DayType.Good;
                            DTD_fair.Probability = decimal.Parse(substrings[2]);
                            DTD_fair.DayType = Enums.DayType.Fair;
                            DTD_poor.Probability = decimal.Parse(substrings[3]);
                            DTD_poor.DayType = Enums.DayType.Poor;

                            DD.DayTypeDistributions.Add(DTD_good);
                            DD.DayTypeDistributions.Add(DTD_fair);
                            DD.DayTypeDistributions.Add(DTD_poor);
                            DemandDistributions.Add(DD);
                            str = SR.ReadLine();
                        continue;
                    }
                }
            }
            SR.Close();

        }

        public void generate_cumulative_range()
        {
            int size = DayTypeDistributions.Count;

            //fill Cumulative column
            DayTypeDistributions[0].CummProbability = DayTypeDistributions[0].Probability;
            for (int i = 1; i < size; i++)
            {
                DayTypeDistributions[i].CummProbability = DayTypeDistributions[i - 1].CummProbability + DayTypeDistributions[i].Probability;
            }
            //fill MinRange , MaxRange
            DayTypeDistributions[0].MinRange = 1;
            DayTypeDistributions[size - 1].MaxRange = 0;
            for (int i = 0; i < size; i++)
            {
                DayTypeDistributions[i].MaxRange = Convert.ToInt32(DayTypeDistributions[i].CummProbability * 100);
            }
            DayTypeDistributions[0].range = Convert.ToString(DayTypeDistributions[0].MinRange) + " - " + Convert.ToString(DayTypeDistributions[0].MaxRange);
            for (int i = 1; i < size; i++)
            {
                DayTypeDistributions[i].MinRange = DayTypeDistributions[i - 1].MaxRange + 1;
                DayTypeDistributions[i].range = Convert.ToString(DayTypeDistributions[i].MinRange) + " - " + Convert.ToString(DayTypeDistributions[i].MaxRange);

            }
        }

        public void generate_cumulative_Demand_range()
        {
            int size = DemandDistributions.Count;
            for (int j = 0; j < 3; j++)
            {
                //fill Cumulative column
                DemandDistributions[0].DayTypeDistributions[j].CummProbability = DemandDistributions[0].DayTypeDistributions[j].Probability;
                for (int i = 1; i < size; i++)
                {
                    DemandDistributions[i].DayTypeDistributions[j].CummProbability = DemandDistributions[i - 1].DayTypeDistributions[j].CummProbability + DemandDistributions[i].DayTypeDistributions[j].Probability;
                }
                //fill MinRange , MaxRange
                DemandDistributions[0].DayTypeDistributions[j].MinRange = 1;
                DemandDistributions[size - 1].DayTypeDistributions[j].MaxRange = 0;
                for (int i = 0; i < size; i++)
                {
                    DemandDistributions[i].DayTypeDistributions[j].MaxRange = Convert.ToInt32(DemandDistributions[i].DayTypeDistributions[j].CummProbability * 100);
                }
                DemandDistributions[0].DayTypeDistributions[j].range = Convert.ToString(DemandDistributions[0].DayTypeDistributions[j].MinRange) + " - " + Convert.ToString(DemandDistributions[0].DayTypeDistributions[j].MaxRange);
                for (int i = 1; i < size; i++)
                {
                    DemandDistributions[i].DayTypeDistributions[j].MinRange = DemandDistributions[i - 1].DayTypeDistributions[j].MaxRange + 1;
                    DemandDistributions[i].DayTypeDistributions[j].range = Convert.ToString(DemandDistributions[i].DayTypeDistributions[j].MinRange) + " - " + Convert.ToString(DemandDistributions[i].DayTypeDistributions[j].MaxRange);

                }
            }
        }

    }
}
