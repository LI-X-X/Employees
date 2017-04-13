using System;


namespace Employees.Data.StoredProcedure
{
    public static class ConstOfStoreProcedures
    {
        public const String PROC_GET_ALL_EMPLOYEES = "GetAllEmployees";//Name of the procedure,which return all employees

        //FOR FIND EMPLOYEES OF THE POSITION
        public const String PROC_GET_EMPLOYEES_BY_POSITION = "GetEmployeesByPosition";//Name of the procedure, which return all employees by position;
        public const String PARAM_POS_FOR_PROC_GET_EMPLOYEES_BY_POSITION = "@position";

        //FOR INSERT EMPLOYEE
        public const String PROC_INSERT_EMPLOYEE = "InsertEmployee";//Name of the procedure, which insert employee;
        public const String PARAM_NAME_FOR_PROC_INSERT_EMPLOYEE = "@name";
        public const String PARAM_LASTNAME_FOR_PROC_INSERT_EMPLOYEE = "@lastName";
        public const String PARAM_POSITION_FOR_PROC_INSERT_EMPLOYEE = "@position";
        public const String PARAM_BIRTHDAY_FOR_PROC_INSERT_EMPLOYEE = "@birthday";
        public const String PARAM_SALARY_FOR_PROC_INSERT_EMPLOYEE = "@salary";

        //FOR DELETE EMPLOYEE
        public const String PROC_DELETE_EMPLOYEE = "DeleteEmployee";//Name of the procedure, which delete employee;
        public const String PARAM_IDEMPLOYEE_FOR_DELETE_EMPLOYEE = "@EmplID";
        public const String PARAM_IDPOSITION_FOR_DELETE_EMPLOYEE = "@PosId";
        public const String PARAM_IDSALARY_FOR_DELETE_EMPLOYEE = "@SalId";

        //FOR AVERAGE SALARY OF POSITION
        public const String PROC_AVERAGE_SALARY_OF_POSITION = "GetAverageSalary";//Name of the procedure, which delete all employee;

    }
}
