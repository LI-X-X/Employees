using Employees.Model.Entities;
using System;

namespace Employees.Model.Validation
{
    public static class ValidatorEmployee
    {
        public static bool IsValid(this Employee emp,out String errorMessage)
        {
            errorMessage = String.Empty;
            if (FieldsNotEmpy(emp,ref errorMessage) && BirthdayInRange(emp,ref errorMessage) && SalaryIsPositive(emp,ref errorMessage))
                return true;
            else
                return false;
        }
        private static bool FieldsNotEmpy(Employee emp, ref String errorMessage) {
            if (!String.IsNullOrEmpty(emp.FirstName) && !String.IsNullOrEmpty(emp.LastName) && !String.IsNullOrEmpty(emp.Birthday) && emp.Positions != null && emp.TotalSalary != 0)
                return true;
            else
            {
                errorMessage = "Not all textbox are full";
                return false;
            }
        }

        private static bool BirthdayInRange(Employee emp, ref String errorMessage) {
            int birthday;
            if (int.TryParse(emp.Birthday, out birthday) && birthday > 1900 && birthday < 2001)
                return true;
            else
            {
                errorMessage = "Birthday is not in range";
                return false;
            }
        }

        private static bool SalaryIsPositive(Employee emp, ref String errorMessage) {
            if (emp.TotalSalary > 0)
                return true;
            else
            {
                errorMessage = "Salary can not be negative";
                return false;
            }
        }
    }
}
