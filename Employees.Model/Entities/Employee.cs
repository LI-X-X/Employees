using System;
using System.Collections.Generic;
using System.Linq;

namespace Employees.Model.Entities
{
    public class Employee : BaseDbEntity
    {
        public Employee()
        {
            Positions = new List<Position>();
        }
        public String FirstName { set; get; }
        public String LastName { set; get; }
        public String Birthday { set; get; }
        public IList<Position> Positions { get; set; }

        public String PositionValues
        {
            get
            {
                return String.Join(",", this.Positions.Select(p => p.Name));
            }
        }

        public Double TotalSalary
        {
            get
            {
                return this.Positions.Sum(p => p.Salary.Value);
            }
        }
    }
}
