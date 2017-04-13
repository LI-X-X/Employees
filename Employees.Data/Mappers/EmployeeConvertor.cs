using System;
using System.Collections.Generic;
using System.Linq;
using Employees.Data.DTOs;
using Employees.Model.Entities;

namespace Employees.Data.Mappers
{
    public class EmployeeConvertor
    {
        public static IEnumerable<Employee> FromDTO(IEnumerable<EmployeeDTO> employeeDtoList)
        {
            var result = new List<Employee>();
            foreach (var dtoItem in employeeDtoList)
            {
                var existed = result.FirstOrDefault(item => item.Id == dtoItem.EmployeeId);

                if (existed != null)
                    existed.Positions.Add(GetPosition(dtoItem, existed));

                else
                {
                    var employee = new Employee();
                    employee.Id = dtoItem.EmployeeId;
                    employee.FirstName = dtoItem.FirstName;
                    employee.LastName = dtoItem.LastName;
                    employee.Birthday = dtoItem.Birthday;
                    employee.Positions.Add(GetPosition(dtoItem, employee));
                    result.Add(employee);
                }
            }
            return result;
        }

        public static EmployeeDTO ToDTO(Employee employee)
        {
            var employeeDTO = new EmployeeDTO();
            employeeDTO.EmployeeId = employee.Id;
            employeeDTO.FirstName = employee.FirstName;
            employeeDTO.LastName = employee.LastName;
            employeeDTO.Birthday = employee.Birthday;
            employeeDTO.PositionId = employee.Positions.First().Id;
            employeeDTO.PositionName = employee.Positions.First().Name;
            employeeDTO.SalaryId = employee.Positions.First().Salary.Id;
            employeeDTO.Salary = (int)employee.TotalSalary;
            return employeeDTO;
        }



        private static Position GetPosition(EmployeeDTO employeeDTO, Employee employee)
        {
            var position = new Position(employee);
            position.Id = employeeDTO.PositionId;
            position.Name = employeeDTO.PositionName;
            var salary = new Salary(position);
            salary.Id = employeeDTO.SalaryId;
            salary.Value = Convert.ToDouble(employeeDTO.Salary);
            return position;
        }


    }
}
