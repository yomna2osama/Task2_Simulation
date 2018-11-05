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
            
            for(int i=0; i<simulation_sys.DemandDistributions.Count;i++)
            {
                Demanddistributions.DataSource=simulation_sys.DemandDistributions[i].DayTypeDistributions;
            }
            //Demanddistributions.Columns.Add(simulation_sys.DemandDistributions);
            
            PerformanceMeasures.DataSource = simulation_sys.PerformanceMeasures;
            SimulationTable.DataSource = simulation_sys.SimulationTable;
        }
    }
}
