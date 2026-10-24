/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;

namespace PayrollSystem
{
    public class HourlyEmployee : Employee
    {
        public decimal HourlyRate { get; private set; }
        public double WorkedHours { get; private set; }

        public HourlyEmployee(string id, string name, decimal hourlyRate, double workedHours)
            : this(id, name, "Unassigned", hourlyRate, workedHours) { }

        public HourlyEmployee(string id, string name, string dept,
            decimal hourlyRate, double workedHours)
            : base(id, name, dept)
        {
            if (hourlyRate < 0)
                throw new ArgumentOutOfRangeException(nameof(hourlyRate), "Don gia gio khong am.");
            if (workedHours < 0 || workedHours > 250)
                throw new ArgumentOutOfRangeException(nameof(workedHours), "Gio lam trong [0, 250].");
            HourlyRate = hourlyRate;
            WorkedHours = workedHours;
        }

        public decimal BasePay()
        {
            if (WorkedHours <= 160)
                return (decimal)WorkedHours * HourlyRate;
            return 160 * HourlyRate + (decimal)(WorkedHours - 160) * HourlyRate * 1.5m;
        }

        public override decimal CalculateGrossPay() => BasePay() + MonthlyBonus;
        public override string GetEmployeeType() => "Hourly";

        public override void DisplayPayrollInfo()
        {
            base.DisplayPayrollInfo();
            Console.WriteLine($"    Gio: {WorkedHours} x {HourlyRate:N0} => Base: {BasePay():N0} + Thuong: {MonthlyBonus:N0} = {CalculateGrossPay():N0}");
        }
    }
}

