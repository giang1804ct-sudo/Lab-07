/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;

namespace PayrollSystem
{
    public class SalesEmployee : Employee
    {
        public decimal BaseSalary { get; private set; }
        public decimal SalesRevenue { get; private set; }
        public decimal CommissionRate { get; private set; }

        public SalesEmployee(string id, string name, decimal baseSalary)
            : this(id, name, "Unassigned", baseSalary, 0m, 0m) { }

        public SalesEmployee(string id, string name, string dept,
            decimal baseSalary, decimal salesRevenue, decimal commissionRate)
            : base(id, name, dept)
        {
            if (baseSalary < 0)
                throw new ArgumentOutOfRangeException(nameof(baseSalary), "Luong co ban khong am.");
            if (salesRevenue < 0)
                throw new ArgumentOutOfRangeException(nameof(salesRevenue), "Doanh so khong am.");
            if (commissionRate < 0 || commissionRate > 0.3m)
                throw new ArgumentOutOfRangeException(nameof(commissionRate), "Hoa hong trong [0, 0.3].");
            BaseSalary = baseSalary;
            SalesRevenue = salesRevenue;
            CommissionRate = commissionRate;
        }

        public void UpdateSales(decimal revenue, decimal rate)
        {
            if (revenue < 0)
                throw new ArgumentOutOfRangeException(nameof(revenue), "Doanh so khong am.");
            if (rate < 0 || rate > 0.3m)
                throw new ArgumentOutOfRangeException(nameof(rate), "Hoa hong trong [0, 0.3].");
            SalesRevenue = revenue;
            CommissionRate = rate;
        }

        public decimal Commission() => SalesRevenue * CommissionRate;

        public override decimal CalculateGrossPay()
            => BaseSalary + Commission() + MonthlyBonus;

        public override string GetEmployeeType() => "Sales";

        public override void DisplayPayrollInfo()
        {
            base.DisplayPayrollInfo();
            Console.WriteLine($"    Co ban: {BaseSalary:N0} + HH: {SalesRevenue:N0} x {CommissionRate:P1} = {Commission():N0} + Thuong: {MonthlyBonus:N0} = {CalculateGrossPay():N0}");
        }
    }
}

