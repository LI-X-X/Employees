using System;
using System.Drawing;
using Employees.Model.Entities;
using System.Windows.Forms;
using Employees.Model.Validation;

namespace Employees
{
    public partial class FormCreateEmployee : Form
    {
        public FormCreateEmployee()
        {
            InitializeComponent();
        }

        public class NewEmployeeEventArgs : EventArgs {
            private readonly Employee employee;
            public NewEmployeeEventArgs(Employee employee) {
                this.employee = employee;
            }
            public Employee EmployeeDTO{ get { return this.employee; } }
        }

        public event EventHandler<NewEmployeeEventArgs> EmployeePassedValidation;

        protected virtual void OnEmployeePassedValidation(NewEmployeeEventArgs e)//Incorrect for multi thread application!
        {
            if (EmployeePassedValidation != null) { EmployeePassedValidation(this, e); }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var createFormEntity = new CreateFormEntity();
            createFormEntity.FirstName = this.textBoxFirstName.Text;
            createFormEntity.LastName = this.textBoxLastName.Text;
            createFormEntity.Birthday = this.textBoxBirthday.Text;
            createFormEntity.Position = this.textBoxPosition.Text;
            createFormEntity.Salary = this.textBoxSalary.Text;
            String errorMessage;
            if (createFormEntity.IsValid(out errorMessage)) {
                var employee = FillEmployee(createFormEntity);
                if (ValidatorEmployee.IsValid(employee, out errorMessage))
                {
                    OnEmployeePassedValidation(new NewEmployeeEventArgs(employee));
                    this.Close();
                }
                else
                {
                    this.labelError.Text = errorMessage;
                    SetPositionLabelError();
                }
            }
            else {
                this.labelError.Text = errorMessage;
                SetPositionLabelError();
            }
        }

        private void SetPositionLabelError() {
            Int32 correct = this.Width / 2 - this.labelError.Width / 2;
            this.labelError.Location = new Point(correct, this.labelError.Location.Y);
        }

        private Employee FillEmployee(CreateFormEntity createFormEntity) {
            var employee = new Employee();
            employee.FirstName = createFormEntity.FirstName;
            employee.LastName = createFormEntity.LastName;
            employee.Birthday = createFormEntity.Birthday;
            var emplPosition = new Position(employee);
            emplPosition.Name = createFormEntity.Position;
            var salareEmpl = new Salary(emplPosition);
            salareEmpl.Value = Convert.ToInt32(createFormEntity.Salary);
            employee.Positions.Add(emplPosition);
            return employee;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
