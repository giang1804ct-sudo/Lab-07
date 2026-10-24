/**********************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/**********************/

using System;

public class HourlyEmployee : Employee 
{
    private double hourlyRate;
    private double workedHours;

    public HourlyEmployee(string id, string name, double rate, double hours) 
        : base(id, name) 
    {
        ValidateAndSet(rate, hours);
    }

    public HourlyEmployee(string id, string name, string dept, double rate, double hours) 
        : base(id, name, dept) 
    {
        ValidateAndSet(rate, hours);
    }

    private void ValidateAndSet(double rate, double hours) 
    {
        if (rate < 0) throw new ArgumentException("Đơn giá giờ không được âm.");
        if (hours < 0 || hours > 250) throw new ArgumentException("Số giờ làm hợp lệ từ 0 đến 250.");
        
        hourlyRate = rate;
        workedHours = hours;
    }

    public override double CalculateGrossPay() 
    {
        double basePay = 0;
        if (workedHours <= 160) {
            basePay = workedHours * hourlyRate;
        } else {
            basePay = 160 * hourlyRate + (workedHours - 160) * hourlyRate * 1.5;
        }
        return basePay + monthlyBonus;
    }

    public override string GetEmployeeType() => "Hourly";
}