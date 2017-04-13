using System;
using System.Collections.Generic;
using Employees.Data.Common;
using Employees.Model;
using Employees.Data.DTOs;
using Employees.Data.Mappers;
using Employees.Model.Entities;
using Employees.Data.StoredProcedure;

namespace Employees.Data.DataManagers
{
    public class DataReaderManager : IDataManager
    {
        private DataReaderExecutor<EmployeeDTO> employees;
        private DataReaderExecutor<SalaryAverageEntity> averageSalaries;
        public DataReaderManager(String connString)
        {
            this.employees = new DataReaderExecutor<EmployeeDTO>(connString);
            this.averageSalaries = new DataReaderExecutor<SalaryAverageEntity>(connString);
        }

        public IEnumerable<Employee> ListEmployees()
        {
            return EmployeeConvertor.FromDTO(this.employees.Get(ConstOfStoreProcedures.PROC_GET_ALL_EMPLOYEES));
        }
        public IEnumerable<Employee> ListEmployeesByPosition(String position) {
            Dictionary<String, String> dict = new Dictionary<String, String> { {ConstOfStoreProcedures.PARAM_POS_FOR_PROC_GET_EMPLOYEES_BY_POSITION,
            position } };
            var param = SqlParametrHelper.FillSqlParametrs(dict);
            return EmployeeConvertor.FromDTO(this.employees.Get(ConstOfStoreProcedures.PROC_GET_EMPLOYEES_BY_POSITION, param));
        }

        public void InsertEmployee(Employee employee)
        {
            var employeeDTO = EmployeeConvertor.ToDTO(employee);
            Dictionary<String, String> dictParams = new Dictionary<String, String> {
                {ConstOfStoreProcedures.PARAM_NAME_FOR_PROC_INSERT_EMPLOYEE,employeeDTO.FirstName },
                {ConstOfStoreProcedures.PARAM_LASTNAME_FOR_PROC_INSERT_EMPLOYEE,employeeDTO.LastName },
                {ConstOfStoreProcedures.PARAM_BIRTHDAY_FOR_PROC_INSERT_EMPLOYEE,employeeDTO.Birthday },
                {ConstOfStoreProcedures.PARAM_POSITION_FOR_PROC_INSERT_EMPLOYEE,employeeDTO.PositionName },
                {ConstOfStoreProcedures.PARAM_SALARY_FOR_PROC_INSERT_EMPLOYEE,employeeDTO.Salary.ToString() } };
            var param = SqlParametrHelper.FillSqlParametrs(dictParams);
            this.employees.ExecuteWithoutResult(param, ConstOfStoreProcedures.PROC_INSERT_EMPLOYEE);
        }

        public void DeleteEmployee(Employee employee)
        {
            var employeeDTO = EmployeeConvertor.ToDTO(employee);
            Dictionary<String, Int32> dictParams = new Dictionary<String, Int32> {
                {ConstOfStoreProcedures.PARAM_IDEMPLOYEE_FOR_DELETE_EMPLOYEE,employeeDTO.EmployeeId},
                {ConstOfStoreProcedures.PARAM_IDPOSITION_FOR_DELETE_EMPLOYEE,employeeDTO.PositionId },
                {ConstOfStoreProcedures.PARAM_IDSALARY_FOR_DELETE_EMPLOYEE,employeeDTO.SalaryId } };
            var param = SqlParametrHelper.FillSqlParametrs(dictParams);
            this.employees.ExecuteWithoutResult(param, ConstOfStoreProcedures.PROC_DELETE_EMPLOYEE);
        }
        public IEnumerable<SalaryAverageEntity> ListSalaryAverage()
        {
            return this.averageSalaries.Get(ConstOfStoreProcedures.PROC_AVERAGE_SALARY_OF_POSITION);
        }

    }
}
