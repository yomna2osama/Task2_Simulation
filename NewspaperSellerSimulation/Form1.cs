using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerTesting;
using NewspaperSellerModels;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable DemandTable = new DataTable();
            DataColumn col;

            string testcase = Constants.FileNames.TestCase1;
            string path = @"E:\year4_1\Simulation\Tasks\Task 2\Task2 - Template\Task2_Simulation\NewspaperSellerSimulation\TestCases\"+testcase;
            SimulationSystem simulation_sys = new SimulationSystem();
            simulation_sys.start_simulation(path);
            label12.Text = simulation_sys.NumOfNewspapers.ToString();
            label11.Text = simulation_sys.NumOfRecords.ToString();
            label4.Text = simulation_sys.PurchasePrice.ToString();
            label10.Text = simulation_sys.ScrapPrice.ToString();
            label14.Text = simulation_sys.SellingPrice.ToString();
            daytypedistributions.DataSource = simulation_sys.DayTypeDistributions;
            daytypedistributions.Columns.Remove("MinRange");
            daytypedistributions.Columns.Remove("MaxRange");

            col = new DataColumn("Demand");
            DemandTable.Columns.Add(col);

            col = new DataColumn("Good Probability");
            DemandTable.Columns.Add(col);
            col = new DataColumn("Fair Probability");
            DemandTable.Columns.Add(col);
            col = new DataColumn("Poor Probability");
            DemandTable.Columns.Add(col);

            col = new DataColumn("Good Range");
            DemandTable.Columns.Add(col);
            col = new DataColumn("Fair Range");
            DemandTable.Columns.Add(col);
            col = new DataColumn("Poor Range");
            DemandTable.Columns.Add(col);

            for (int i = 0; i < simulation_sys.DemandDistributions.Count; i++)
            {
                DemandTable.Rows.Add(simulation_sys.DemandDistributions[i].Demand, simulation_sys.DemandDistributions[i].DayTypeDistributions[0].Probability, simulation_sys.DemandDistributions[i].DayTypeDistributions[1].Probability, simulation_sys.DemandDistributions[i].DayTypeDistributions[2].Probability, simulation_sys.DemandDistributions[i].DayTypeDistributions[0].range, simulation_sys.DemandDistributions[i].DayTypeDistributions[1].range, simulation_sys.DemandDistributions[i].DayTypeDistributions[2].range);
            }

            Demanddistributions.DataSource = DemandTable;
            PerformanceMeasures.DataSource = simulation_sys.PerformanceMeasures;
            SimulationTable.DataSource = simulation_sys.SimulationTable;
        }
    }
}
