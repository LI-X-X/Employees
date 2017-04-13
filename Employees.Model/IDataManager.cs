using System;
using System.Collections.Generic;
using Employees.Model.Entities;

namespace Employees.Model
{
    public interface IDataManager
    {
        IEnumerable<Employee> ListEmployees();
        IEnumerable<Employee> ListEmployeesByPosition(String position);
        void InsertEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        IEnumerable<SalaryAverageEntity> ListSalaryAverage();

    }
}
