/**********************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/**********************/

using System;

public abstract class Employee 
{
    protected string employeeId;
    protected string fullName;
    protected string department;
    protected double monthlyBonus;

    public Employee(string id, string name) 
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Mã nhân sự không rỗng.");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Họ tên không rỗng.");
        
        employeeId = id;
        fullName = name;
        department = "Unassigned"; 
        monthlyBonus = 0;
    }

    public Employee(string id, string name, string dept) : this(id, name) 
    {
        if (string.IsNullOrWhiteSpace(dept)) throw new ArgumentException("Phòng ban không rỗng.");
        department = dept;
    }

    public string Id => employeeId;
    public string Department => department;

    public void AddBonus(double amount) 
    {
        if (amount <= 0) throw new ArgumentException("Số tiền thưởng phải > 0.");
        monthlyBonus += amount;
    }

    public void AddBonus(double amount, string reason) 
    {
        if (amount <= 0) throw new ArgumentException("Số tiền thưởng phải > 0.");
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Lý do không được rỗng.");
        monthlyBonus += amount;
    }

    public void AddBonus(double rate, double referenceAmount, string reason) 
    {
        if (rate <= 0 || rate > 0.5) throw new ArgumentException("Tỷ lệ thưởng phải nằm trong khoảng (0, 0.5].");
        if (referenceAmount <= 0) throw new ArgumentException("Giá trị tham chiếu phải > 0.");
        if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Lý do không được rỗng.");
        monthlyBonus += (rate * referenceAmount);
    }

    public abstract double CalculateGrossPay();
    public abstract string GetEmployeeType();

    public virtual void DisplayPayrollInfo() 
    {
        Console.WriteLine($"[{GetEmployeeType()}] ID: {employeeId} | Tên: {fullName} | Phòng: {department} | Thưởng: {monthlyBonus:N0} | Thu nhập: {CalculateGrossPay():N0}");
    }
}