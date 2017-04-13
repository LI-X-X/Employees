using System;
namespace Employees.Data.DTOs
{
    public class EmployeeDTO
    {
        public Int32 EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Birthday { get; set; }
        public Int32 PositionId { get; set; }
        public String PositionName { get; set; }
        public Int32 SalaryId { get; set; }
        public Int32 Salary { get; set; }
    }
}
