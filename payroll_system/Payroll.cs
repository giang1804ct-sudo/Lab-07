/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem
{
    public class Payroll
    {
        public string Period { get; }
        private readonly List<Employee> _employees = new();

        public Payroll(string period)
        {
            if (string.IsNullOrWhiteSpace(period))
                throw new ArgumentException("Ky luong khong duoc rong.");
            Period = period.Trim();
        }

        public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            if (_employees.Any(e => e.EmployeeId == employee.EmployeeId))
                throw new InvalidOperationException($"Trung ma nhan su: {employee.EmployeeId}.");
            _employees.Add(employee);
        }

        public Employee? FindEmployee(string employeeId)
            => _employees.FirstOrDefault(e => e.EmployeeId == employeeId);

        public decimal CalculateTotalPayroll()
        {
            decimal total = 0m;
            foreach (var e in _employees)
                total += e.CalculateGrossPay();
            return total;
        }

        public decimal CalculatePayrollByDepartment(string department)
        {
            decimal total = 0m;
            foreach (var e in _employees)
                if (string.Equals(e.Department, department, StringComparison.OrdinalIgnoreCase))
                    total += e.CalculateGrossPay();
            return total;
        }

        public Employee? FindHighestPaidEmployee()
        {
            if (_employees.Count == 0) return null;
            Employee best = _employees[0];
            foreach (var e in _employees)
                if (e.CalculateGrossPay() > best.CalculateGrossPay())
                    best = e;
            return best;
        }

        public void DisplayPayroll()
        {
            Console.WriteLine($"===== BANG LUONG KY {Period} =====");
            if (_employees.Count == 0)
            {
                Console.WriteLine("(danh sach rong)");
                return;
            }
            foreach (var e in _employees)
                e.DisplayPayrollInfo();
            Console.WriteLine($"Tong cong: {CalculateTotalPayroll():N0}");
        }
    }
}

