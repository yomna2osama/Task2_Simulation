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
            generate_cumulative_range(DayTypeDistributions);
            fill_DemandDistributions();
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

        public void generate_cumulative_range(List<DayTypeDistribution> dist)
        {
            int size = dist.Count;

            //fill Cumulative column
            dist[0].CummProbability = dist[0].Probability;
            for (int i = 1; i < size; i++)
            {
                dist[i].CummProbability = dist[i - 1].CummProbability + dist[i].Probability;
            }
            //fill MinRange , MaxRange
            dist[0].MinRange = 1;
            dist[size - 1].MaxRange = 0;
            for (int i = 0; i < size; i++)
            {
                dist[i].MaxRange = Convert.ToInt32(dist[i].CummProbability * 100);
            }
            dist[0].range = Convert.ToString(dist[0].MinRange) + " - " + Convert.ToString(dist[0].MaxRange);
            for (int i = 1; i < size; i++)
            {
                dist[i].MinRange = dist[i - 1].MaxRange + 1;
                dist[i].range = Convert.ToString(dist[i].MinRange) + " - " + Convert.ToString(dist[i].MaxRange);

            }
        }

        public void fill_DemandDistributions()
        {
            for(int i=0;i<DemandDistributions.Count;i++)
            {
                generate_cumulative_range(DemandDistributions[i].DayTypeDistributions);
            }
        }
    }
}
