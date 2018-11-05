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
                    PurchasePrice = int.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "ScrapPrice")
                {
                    ScrapPrice = int.Parse(SR.ReadLine());
                    SR.ReadLine();
                    continue;
                }
                else if (str == "SellingPrice")
                {
                    SellingPrice = int.Parse(SR.ReadLine());
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
                            DTD_good.DayType = (Enums.DayType)0;
                            DTD_fair.Probability = decimal.Parse(substrings[2]);
                            DTD_fair.DayType = (Enums.DayType)1;
                            DTD_poor.Probability = decimal.Parse(substrings[3]);
                            DTD_poor.DayType = (Enums.DayType)2;

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
    }
}
