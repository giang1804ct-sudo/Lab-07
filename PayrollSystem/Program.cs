/**********************/
// Mã sinh viên: 202418889
// Họ tên: Đặng Thị Hương Giang
/**********************/

using System;

class HeThongLuongMain 
{
    static void Main() 
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; 
        
        Console.WriteLine("==================================================");
        Console.WriteLine("       PHẦN 1: BÁO CÁO THU NHẬP NHÂN SỰ");
        Console.WriteLine("==================================================");
        
        Payroll payroll = new Payroll("Tháng 09/2026");

        // Khởi tạo nhân sự theo đúng đề bài
        Employee emp1 = new SalariedEmployee("E001", "Nguyễn An Khang", "Đào tạo", 15000000, 2000000);
        emp1.AddBonus(1000000); 
        payroll.AddEmployee(emp1);

        Employee emp2 = new HourlyEmployee("E002", "Trần Mai Anh", "Hỗ trợ", 100000, 150);
        emp2.AddBonus(500000, "Thưởng dự án"); 
        payroll.AddEmployee(emp2);

        Employee emp3 = new HourlyEmployee("E003", "Mai Anh Chi", "Hỗ trợ", 100000, 170);
        payroll.AddEmployee(emp3);

        Employee emp4 = new SalesEmployee("E004", "Đặng Văn Nam", "Kinh doanh", 8000000, 200000000, 0.05);
        emp4.AddBonus(0.02, 50000000, "Thưởng theo KPI"); 
        payroll.AddEmployee(emp4);

        // Hiển thị bảng lương
        payroll.DisplayPayroll();
        
        Console.WriteLine($">> Thống kê quỹ lương: {payroll.CalculateTotalPayroll():N0} VNĐ");
        Console.WriteLine($">> Quỹ lương ban Hỗ trợ: {payroll.CalculatePayrollByDepartment("Hỗ trợ"):N0} VNĐ");
        
        var topEarner = payroll.FindHighestPaidEmployee();
        if(topEarner != null) {
            Console.WriteLine($">> Nhân sự lương cao nhất: {topEarner.Id} (Đạt mức {topEarner.CalculateGrossPay():N0} VNĐ)");
        }

        Console.WriteLine("\n==================================================");
        Console.WriteLine("  PHẦN 2: NHẬT KÝ KIỂM THỬ HỆ THỐNG (10 TEST CASES)");
        Console.WriteLine("==================================================");
        
        int passedTests = 0; // Biến đếm số bài test chạy đúng kịch bản

        // Test 1: Trùng ID
        Console.Write($"Test #01 | {"Thêm nhân sự trùng ID",-26} : ");
        try { 
            payroll.AddEmployee(emp1); 
            Console.WriteLine("X LỖI (Hệ thống không chặn được)");
        } catch (Exception ex) { 
            Console.WriteLine($"[OK] -> {ex.Message}"); 
            passedTests++;
        }

        // Hàm hỗ trợ test tự động (Giao diện mới hoàn toàn)
        void ExecuteTest(int testId, string description, Action testTask)
        {
            Console.Write($"Test #{testId:D2} | {description,-26} : ");
            try {
                testTask();
                Console.WriteLine("X LỖI (Hệ thống không chặn được)");
            } catch (Exception ex) {
                Console.WriteLine($"[OK] -> {ex.Message}");
                passedTests++;
            }
        }

        // Thực thi Test 2 đến Test 10
        ExecuteTest(2, "Khởi tạo nhân viên mã rỗng", () => new SalariedEmployee("", "Lỗi", 1000, 100));
        ExecuteTest(3, "Nhập mức lương âm", () => new SalariedEmployee("E005", "Lỗi", -5000, 1000));
        ExecuteTest(4, "Nhập số tiền thưởng âm", () => emp1.AddBonus(-1000));
        ExecuteTest(5, "Nhập số giờ làm việc âm", () => new HourlyEmployee("E006", "Lỗi", 100000, -5));
        ExecuteTest(6, "Số giờ vượt ngưỡng 250", () => new HourlyEmployee("E007", "Lỗi", 100000, 260));
        ExecuteTest(7, "Tỷ lệ hoa hồng âm", () => new SalesEmployee("E008", "Lỗi", 5000, 10000, -0.1));
        ExecuteTest(8, "Tỷ lệ hoa hồng vượt 30%", () => new SalesEmployee("E009", "Lỗi", 5000, 10000, 0.4));
        ExecuteTest(9, "Tỷ lệ thưởng lớn hơn 50%", () => emp1.AddBonus(0.6, 100000, "Lý do"));
        ExecuteTest(10, "Thưởng không ghi rõ lý do", () => emp1.AddBonus(10000, ""));

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"=> TỔNG KẾT: Đã vượt qua thành công {passedTests}/10 bài kiểm thử biên.");
    }
}