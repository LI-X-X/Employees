using System;
using Employees.Model;
using System.Windows.Forms;
using Employees.Model.Entities;
using Employees.Report;

namespace Employees
{
    public partial class FormEmployees : Form
    {
        private IDataManager dataManager;
        private IDataReport reportManager;
        public FormEmployees(IDataManager dataManager, IDataReport reportManager)
        {
            InitializeComponent();
            this.reportManager = reportManager;
            this.dataManager = dataManager;
            reportManager.ReportManagerFinished += ReportManager_ReportManagerFinished;
        }

        private void FormEmployees_Load(object sender, EventArgs e)
        {
            ConfigureDataGridViewColumns();
            this.dataGridViewEmployees.DataSource = this.dataManager.ListEmployees();
        }
        private void ConfigureDataGridViewColumns()
        {
            this.dataGridViewEmployees.AutoGenerateColumns = false;

            var firstNameColumn = new DataGridViewTextBoxColumn();
            firstNameColumn.HeaderText = "First Name";
            firstNameColumn.DataPropertyName = "FirstName";
            dataGridViewEmployees.Columns.Add(firstNameColumn);

            var lastNameColumn = new DataGridViewTextBoxColumn();
            lastNameColumn.HeaderText = "Last Name";
            lastNameColumn.DataPropertyName = "LastName";
            dataGridViewEmployees.Columns.Add(lastNameColumn);

            var birthdayColumn = new DataGridViewTextBoxColumn();
            birthdayColumn.HeaderText = "Birthday";
            birthdayColumn.DataPropertyName = "Birthday";
            dataGridViewEmployees.Columns.Add(birthdayColumn);

            var positionColumn = new DataGridViewTextBoxColumn();
            positionColumn.HeaderText = "Positions";
            positionColumn.DataPropertyName = "PositionValues";
            dataGridViewEmployees.Columns.Add(positionColumn);

            var salaryColumn = new DataGridViewTextBoxColumn();
            salaryColumn.HeaderText = "Total Salary";
            salaryColumn.DataPropertyName = "TotalSalary";
            dataGridViewEmployees.Columns.Add(salaryColumn);
        }


        private void buttonFind_Click(object sender, EventArgs e)
        {
            var position = this.textBoxForSearch.Text;
            this.dataGridViewEmployees.DataSource = this.dataManager.ListEmployeesByPosition(position);
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var createForm = new FormCreateEmployee();
            createForm.EmployeePassedValidation += new EventHandler<FormCreateEmployee.NewEmployeeEventArgs>(FormCreateEmployee_EmployeePassedValidation);
            createForm.ShowDialog(this);
        }
        private void FormCreateEmployee_EmployeePassedValidation(object sender, FormCreateEmployee.NewEmployeeEventArgs e) {
            dataManager.InsertEmployee(e.EmployeeDTO);
            this.dataGridViewEmployees.DataSource = this.dataManager.ListEmployees();
        }

        private void buttonClearFilter_Click(object sender, EventArgs e)
        {
            this.dataGridViewEmployees.DataSource = this.dataManager.ListEmployees();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var emplForDel = this.dataGridViewEmployees.SelectedRows[0].DataBoundItem as Employee;
            String message = String.Format("Are you sure that you want to delete:{0}Name:{1};{0}LastName:{2};{0}Position:{3}", Environment.NewLine, emplForDel.FirstName, emplForDel.LastName, emplForDel.PositionValues);
            String caption = "Accept";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.dataManager.DeleteEmployee(emplForDel);
            }
            this.dataGridViewEmployees.DataSource = this.dataManager.ListEmployees();

        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            this.reportManager.AverageSalaries = this.dataManager.ListSalaryAverage();
            this.Enabled = false;
            this.reportManager.ExecuteReportAverageSalary();
        }

        private void ReportManager_ReportManagerFinished(object sender, EventArgs e)
        {
            this.Enabled = true;
        }

    }
}
