/**********************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/**********************/

using System;

public class SalesEmployee : Employee 
{
    private double baseSalary;
    private double salesRevenue;
    private double commissionRate;

    public SalesEmployee(string id, string name, double salary, double revenue, double rate) 
        : base(id, name) 
    {
        ValidateAndSet(salary, revenue, rate);
    }

    public SalesEmployee(string id, string name, string dept, double salary, double revenue, double rate) 
        : base(id, name, dept) 
    {
        ValidateAndSet(salary, revenue, rate);
    }

    private void ValidateAndSet(double salary, double revenue, double rate) 
    {
        if (salary < 0) throw new ArgumentException("Lương cơ bản không được âm.");
        if (revenue < 0) throw new ArgumentException("Doanh số không được âm.");
        if (rate < 0 || rate > 0.3) throw new ArgumentException("Tỷ lệ hoa hồng phải nằm trong khoảng từ 0 đến 0.3.");
        
        baseSalary = salary;
        salesRevenue = revenue;
        commissionRate = rate;
    }

    public void UpdateSalesRevenue(double additionalRevenue) 
    {
        if (additionalRevenue < 0) throw new ArgumentException("Doanh số cộng thêm không được âm.");
        salesRevenue += additionalRevenue;
    }

    public override double CalculateGrossPay() 
    {
        return baseSalary + (salesRevenue * commissionRate) + monthlyBonus;
    }

    public override string GetEmployeeType() => "Sales";
}