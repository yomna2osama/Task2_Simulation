using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public class SimulationCase
    {
        public int DayNo { get; set; }///
        public int RandomNewsDayType { get; set; }///
        public Enums.DayType NewsDayType { get; set; }
        public int RandomDemand { get; set; }///
        public int Demand { get; set; }
        public decimal DailyCost { get; set; }
        public decimal SalesProfit { get; set; }
        public decimal LostProfit { get; set; }
        public decimal ScrapProfit { get; set; }
        public decimal DailyNetProfit { get; set; }

        public void set_dem_type(int dayno,List<DemandDistributions> dem_distable,List<DayTypeDistribution> day_distable)
        {
            this.DayNo = dayno;
            Random rand = new Random();
            this.RandomNewsDayType = rand.Next(1, 101);
            this.RandomDemand = rand.Next(1, 101);
            for (int i = 0; i < day_distable.Count; i++)
            {
                if (this.RandomNewsDayType >= day_distable[i].MinRange && this.RandomNewsDayType < day_distable[i].MaxRange)
                {
                    this.NewsDayType = day_distable[i].DayType;
                    break;
                }
            }
            for (int i = 0; i < dem_distable.Count; i++)
            {
                    if (this.RandomDemand >= dem_distable[i].DayTypeDistributions[(int)this.NewsDayType].MinRange && this.RandomDemand < dem_distable[i].DayTypeDistributions[(int)this.NewsDayType].MaxRange)
                    {
                        this.Demand = dem_distable[i].Demand;
                        break;
                    }
            }
        }
        public void calculate_costs(int Supply, int NumOfRecords, decimal PurchaseP, decimal ScrapP, decimal SellingP)
        {
            this.DailyCost = Supply * PurchaseP;
            if (Demand >= Supply)
            {
                this.SalesProfit = Supply * SellingP;
                this.LostProfit = (Demand - Supply) * (SellingP - PurchaseP);
                this.ScrapProfit = 0;
            }
            if (Demand < Supply)
            {
                this.SalesProfit = Supply * SellingP;
                this.LostProfit = 0;
                this.ScrapProfit = (Supply - Demand) * ScrapP;
            }
            this.DailyNetProfit = this.SalesProfit - this.DailyCost - this.LostProfit + this.ScrapProfit;
        }
        
    }
}
