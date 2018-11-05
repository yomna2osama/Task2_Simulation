using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class PerformanceMeasures
    {
        public decimal TotalSalesProfit { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalLostProfit { get; set; }
        public decimal TotalScrapProfit { get; set; }
        public decimal TotalNetProfit { get; set; }
        public int DaysWithMoreDemand { get; set; }
        public int DaysWithUnsoldPapers { get; set; }

        public void calculate_perfrmanceMeasr(List<SimulationCase> simulation_table)
        {
            this.TotalSalesProfit = 0;
            this.TotalCost = 0;
            this.TotalLostProfit = 0;
            this.TotalScrapProfit = 0;
            this.TotalNetProfit = 0;
            this.DaysWithMoreDemand = 0;
            this.DaysWithUnsoldPapers = 0;

            for (int i = 0; i < simulation_table.Count; i++)
            {
                this.TotalCost += simulation_table[i].DailyCost;
                this.TotalSalesProfit+= simulation_table[i].SalesProfit;
                if (simulation_table[i].LostProfit != 0)
                {
                    this.TotalLostProfit += simulation_table[i].LostProfit;
                    this.DaysWithMoreDemand++;
                }
                if (simulation_table[i].ScrapProfit != 0)
                {
                    this.TotalScrapProfit += simulation_table[i].ScrapProfit;
                    this.DaysWithUnsoldPapers++;
                }
                this.TotalNetProfit += simulation_table[i].DailyNetProfit;
            }
        
        }
    }
}
