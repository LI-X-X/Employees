using System;


namespace Employees.Model.Entities
{
     public class CreateFormEntity
    {
        public CreateFormEntity() { }
        public String FirstName { set; get;}
        public String LastName { set; get; }

        public String Birthday { set; get; }

        public String Position { set; get; }

        public String Salary { set; get; }

        public bool IsValid(out String errorMessage) {
            errorMessage = String.Empty;
            if (IsNotNull(ref errorMessage) && SalaryIsValyd(ref errorMessage))
                return true;
            else
                return false;
        }
        private bool IsNotNull(ref String errorMessage) {
            if (!String.IsNullOrEmpty(FirstName) && !String.IsNullOrEmpty(LastName) && !String.IsNullOrEmpty(Birthday) && !String.IsNullOrEmpty(Position) && !String.IsNullOrEmpty(Salary))
                return true;
            else
            {
                errorMessage = "Not all textbox are full";
                return false;
            }
        }

        private bool SalaryIsValyd(ref String errorMessage) {
            double doubleSalary;
            if (double.TryParse(Salary, out doubleSalary))
                return true;
            else
            {
                errorMessage = "Salary is not digit";
                return false;
            }
        }

    }
}
