/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;
using System.Collections.Generic;

namespace PayrollSystem
{
    public abstract class Employee
    {
        private string _employeeId = "";
        private string _fullName = "";
        private string _department = "Unassigned";
        private decimal _monthlyBonus;
        private readonly List<BonusRecord> _history = new();

        public string EmployeeId => _employeeId;
        public string FullName => _fullName;
        public string Department => _department;
        public decimal MonthlyBonus => _monthlyBonus;
        public IReadOnlyList<BonusRecord> BonusHistory => _history.AsReadOnly();

        protected Employee(string employeeId, string fullName)
            : this(employeeId, fullName, "Unassigned") { }

        protected Employee(string employeeId, string fullName, string department)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                throw new ArgumentException("Ma nhan su khong duoc rong.");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Ho ten khong duoc rong.");
            if (string.IsNullOrWhiteSpace(department))
                throw new ArgumentException("Phong ban khong duoc rong.");
            _employeeId = employeeId.Trim();
            _fullName = fullName.Trim();
            _department = department.Trim();
            _monthlyBonus = 0m;
        }

        public void AddBonus(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount phai > 0.");
            _monthlyBonus += amount;
            _history.Add(new BonusRecord(amount, null, "AddBonus(amount)"));
        }

        public void AddBonus(decimal amount, string reason)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "amount phai > 0.");
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Ly do khong duoc rong.");
            _monthlyBonus += amount;
            _history.Add(new BonusRecord(amount, reason.Trim(), "AddBonus(amount, reason)"));
        }

        public void AddBonus(decimal rate, decimal referenceAmount, string reason)
        {
            if (rate <= 0 || rate > 0.5m)
                throw new ArgumentOutOfRangeException(nameof(rate), "rate trong (0, 0.5].");
            if (referenceAmount <= 0)
                throw new ArgumentOutOfRangeException(nameof(referenceAmount), "referenceAmount > 0.");
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Ly do khong duoc rong.");
            decimal computed = rate * referenceAmount;
            AddBonus(computed, $"{reason.Trim()} (ty le {rate:P1} x {referenceAmount:N0})");
        }

        public void ResetBonus()
        {
            _monthlyBonus = 0m;
            _history.Clear();
        }

        public abstract decimal CalculateGrossPay();
        public abstract string GetEmployeeType();

        public virtual void DisplayPayrollInfo()
        {
            Console.WriteLine($"[{GetEmployeeType()}] {EmployeeId} - {FullName} | Phong: {Department} | Thuong: {MonthlyBonus:N0} | Tong: {CalculateGrossPay():N0}");
        }
    }
}

