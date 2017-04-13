using System;

namespace Employees.Model.Entities
{
    public class Salary : BaseDbEntity
    {
        private Salary()
        {

        }

        public Salary(Position position)
        {
            this.Position = position;
            this.Position.Salary = this;
        }
        public Double Value { get; set; }
        public Position Position { get; private set; }
    }
}
