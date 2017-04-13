using System;
using Employees.Model;
using System.Windows.Forms;
using System.Configuration;
using Employees.Data.DataManagers;
using Employees.Report.ReportManagers;

namespace Employees.UI
{
    static class Program
    {
        private const String DATAREADER_DATA_ACCESS_TYPE = "DATAREADER";
        private const String DATASET_DATA_ACCESS_TYPE = "DATASET";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            String strConnect = ConfigurationManager.AppSettings["ConnectionString"];
            String dataType = ConfigurationManager.AppSettings["DataAccessType"];
            IDataManager dataManager;
            try
            {
                dataManager = GetDataManager(dataType, strConnect);
                Application.Run(new FormEmployees(dataManager,new ExcelReportManager()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static IDataManager GetDataManager(String connectionTypeKey, String connectionString)
        {
            if (String.Compare(connectionTypeKey, DATAREADER_DATA_ACCESS_TYPE, true) == 0)
            {
                return new DataReaderManager(connectionString);
            }
            if (String.Compare(connectionTypeKey, DATASET_DATA_ACCESS_TYPE, true) == 0)
            {
                return new DataSetManager(connectionString);
            }

            throw new ArgumentException("Unknown data access type.");
        }
    }
}
