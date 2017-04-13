using System;
using System.Collections.Generic;
using Employees.Model.Entities;


namespace Employees.Report
{
    public interface IDataReport
    {
        IEnumerable<SalaryAverageEntity> AverageSalaries { set; get; }
        void ExecuteReportAverageSalary();
        event EventHandler ReportManagerFinished;
    }
}
