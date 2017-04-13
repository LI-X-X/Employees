using System;

namespace Employees.Model.Entities
{
    public class Position : BaseDbEntity
    {
        private Position()
        {

        }

        public Position(Employee employee)
        {
            this.Employee = employee;
        }
        public String Name { get; set; }

        public Salary Salary { get; set; }

        public Employee Employee { get; set; }
    }
}
