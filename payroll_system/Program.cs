/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;
using PayrollSystem;

var payroll = new Payroll("2026-09");
var e001 = new SalariedEmployee("E001", "Nguyen Minh An", "Ho tro", 14000000m, 1000000m);
e001.AddBonus(500000m, "Hoan thanh tot");
var e002 = new HourlyEmployee("E002", "Tran Thi Bich", "Ho tro", 100000m, 165);
e002.AddBonus(750000m, "Ho tro tang ca");
var e003 = new SalesEmployee("E003", "Le Van Cuong", "Kinh doanh", 8000000m, 50000000m, 0.2m);
e003.AddBonus(0.01m, 50000000m, "Vuot chi tieu");
var e004 = new SalesEmployee("E004", "Pham Thi Dung", "Kinh doanh", 8000000m, 50000000m, 0.2m);
e004.AddBonus(500000m);
payroll.AddEmployee(e001);
payroll.AddEmployee(e002);
payroll.AddEmployee(e003);
payroll.AddEmployee(e004);
payroll.DisplayPayroll();
Console.WriteLine($"Tong Ho tro: {payroll.CalculatePayrollByDepartment("Ho tro"):N0}");
Console.WriteLine($"Tong Kinh doanh: {payroll.CalculatePayrollByDepartment("Kinh doanh"):N0}");
var best = payroll.FindHighestPaidEmployee();
Console.WriteLine($"Cao nhat: {best?.EmployeeId} - {best?.CalculateGrossPay():N0}");
Console.WriteLine();
TestRunner.RunAll();


