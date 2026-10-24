/**********************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/**********************/

using System;

public class SalariedEmployee : Employee 
{
    private double monthlySalary;
    private double responsibilityAllowance;

    public SalariedEmployee(string id, string name, double salary, double allowance) 
        : base(id, name) 
    {
        ValidateAndSet(salary, allowance);
    }

    public SalariedEmployee(string id, string name, string dept, double salary, double allowance) 
        : base(id, name, dept) 
    {
        ValidateAndSet(salary, allowance);
    }

    private void ValidateAndSet(double salary, double allowance) 
    {
        if (salary < 0) throw new ArgumentException("Lương tháng không được âm.");
        if (allowance < 0) throw new ArgumentException("Phụ cấp trách nhiệm không được âm.");
        
        monthlySalary = salary;
        responsibilityAllowance = allowance;
    }

    public override double CalculateGrossPay() 
    {
        return monthlySalary + responsibilityAllowance + monthlyBonus;
    }

    public override string GetEmployeeType() => "Salaried";
}