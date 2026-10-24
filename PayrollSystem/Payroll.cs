/**********************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/**********************/

using System;
using System.Collections.Generic;
using System.Linq;

public class Payroll 
{
    private string period;
    private List<Employee> employees;

    public Payroll(string period) 
    {
        this.period = period;
        this.employees = new List<Employee>();
    }

    public void AddEmployee(Employee emp) 
    {
        if (employees.Exists(e => e.Id == emp.Id)) {
            throw new ArgumentException($"Nhân sự mã {emp.Id} đã tồn tại trong bảng lương.");
        }
        employees.Add(emp);
    }

    public double CalculateTotalPayroll() 
    {
        return employees.Sum(e => e.CalculateGrossPay());
    }

    public double CalculatePayrollByDepartment(string dept) 
    {
        return employees.Where(e => e.Department == dept).Sum(e => e.CalculateGrossPay());
    }

    
    public Employee? FindHighestPaidEmployee()
    {
        if (employees.Count == 0) return null;
        return employees.OrderByDescending(e => e.CalculateGrossPay()).First();
    }

    public void DisplayPayroll() 
    {
        Console.WriteLine($"\n--- BẢNG LƯƠNG KỲ: {period} ---");
        foreach (var emp in employees) {
            emp.DisplayPayrollInfo(); 
        }
        Console.WriteLine("----------------------------------");
    }
}