using System;
using Employees.Model.Entities;
using Employees.Data.Common;
using Employees.Data.StoredProcedure;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;


namespace Employees.Report.ReportManagers
{
    public class ExcelReportManager:IDataReport
    {

        public IEnumerable<SalaryAverageEntity> AverageSalaries { set; get; }
        public ExcelReportManager() {

        }
        public void ExecuteReportAverageSalary() {
            Application excelApp = new Application();
            SetupAndFillExcele(excelApp, AverageSalaries);
        }


        private void SetupAndFillExcele(Application ExcelApp,IEnumerable<SalaryAverageEntity> list) {
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 20;
            ExcelApp.Cells[1, 1] = "Position Name";
            ExcelApp.Cells[1, 2] = "Average Salary";
            Int32 i = 0;
            foreach (var item in list)
            {
                ExcelApp.Cells[2 + i, 1] = item.Position;
                ExcelApp.Cells[2 + i, 2] = item.AverageSalary;
                ++i;
            }
            ExcelApp.Visible = true;
            OnReportManagerFinished(new NewWorkSheetFinishedEventArgs());
        }

        public class NewWorkSheetFinishedEventArgs : EventArgs { }

        public event EventHandler ReportManagerFinished;

        protected virtual void OnReportManagerFinished(NewWorkSheetFinishedEventArgs e)//Incorrect for multi thread application!
        {
            if (ReportManagerFinished != null)
            {
                ReportManagerFinished(this, e);
            }
        }
    }
}
