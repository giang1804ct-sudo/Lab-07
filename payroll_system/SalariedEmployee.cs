/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;

namespace PayrollSystem
{
    public class SalariedEmployee : Employee
    {
        public decimal MonthlySalary { get; private set; }
        public decimal ResponsibilityAllowance { get; private set; }

        public SalariedEmployee(string id, string name, decimal monthlySalary)
            : this(id, name, "Unassigned", monthlySalary, 0m) { }

        public SalariedEmployee(string id, string name, string dept,
            decimal monthlySalary, decimal allowance)
            : base(id, name, dept)
        {
            if (monthlySalary < 0)
                throw new ArgumentOutOfRangeException(nameof(monthlySalary), "Luong co dinh khong am.");
            if (allowance < 0)
                throw new ArgumentOutOfRangeException(nameof(allowance), "Phu cap khong am.");
            MonthlySalary = monthlySalary;
            ResponsibilityAllowance = allowance;
        }

        public override decimal CalculateGrossPay()
            => MonthlySalary + ResponsibilityAllowance + MonthlyBonus;

        public override string GetEmployeeType() => "Salaried";

        public override void DisplayPayrollInfo()
        {
            base.DisplayPayrollInfo();
            Console.WriteLine($"    Luong: {MonthlySalary:N0} + Phu cap: {ResponsibilityAllowance:N0} + Thuong: {MonthlyBonus:N0} = {CalculateGrossPay():N0}");
        }
    }
}

