/***************
Ma sinh vien: 202418889
Ho ten: Dang Thi Huong Giang
Bai 04 - He thong tinh luong va thuong nhan su
***************/
using System;

namespace PayrollSystem
{
    public static class TestRunner
    {
        private static int _pass;
        private static int _fail;

        private static void Check(string name, decimal actual, decimal expected)
        {
            if (actual == expected)
            {
                _pass++;
                Console.WriteLine($"PASS {name}: {actual:N0}");
            }
            else
            {
                _fail++;
                Console.WriteLine($"FAIL {name}: got {actual:N0}, expected {expected:N0}");
            }
        }

        private static void ExpectThrow(string name, Action act)
        {
            try
            {
                act();
                _fail++;
                Console.WriteLine($"FAIL {name}: khong nem ngoai le");
            }
            catch (Exception ex)
            {
                _pass++;
                Console.WriteLine($"PASS {name}: nem {ex.GetType().Name}");
            }
        }

        public static void RunAll()
        {
            Console.WriteLine("===== KIEM THU BIEN VA LOI (>=10 tinh huong) =====");

            var s1 = new SalariedEmployee("T01", "A", "Ho tro", 14000000m, 1000000m);
            s1.AddBonus(500000m);
            Check("T01 Salaried 14M+1M+0.5M", s1.CalculateGrossPay(), 15500000m);

            var h160 = new HourlyEmployee("T02", "B", "Ho tro", 100000m, 160);
            Check("T02 Hourly dung 160h", h160.CalculateGrossPay(), 16000000m);

            var h161 = new HourlyEmployee("T03", "C", "Ho tro", 100000m, 161);
            Check("T03 Hourly 161h tang ca", h161.CalculateGrossPay(), 16150000m);

            var h0 = new HourlyEmployee("T04", "D", "Ho tro", 100000m, 0);
            Check("T04 Hourly 0h", h0.CalculateGrossPay(), 0m);

            var h250 = new HourlyEmployee("T05", "E", "Ho tro", 100000m, 250);
            Check("T05 Hourly max 250h", h250.CalculateGrossPay(), 29500000m);

            ExpectThrow("T06 Hourly 251h", () => new HourlyEmployee("X", "X", 100000m, 251));
            ExpectThrow("T07 Hourly -1h", () => new HourlyEmployee("X", "X", 100000m, -1));

            var sa0 = new SalesEmployee("T08", "F", "Kinh doanh", 8000000m, 50000000m, 0m);
            Check("T08 Sales rate 0", sa0.CalculateGrossPay(), 8000000m);

            var saMax = new SalesEmployee("T09", "G", "Kinh doanh", 8000000m, 50000000m, 0.3m);
            Check("T09 Sales rate 0.3 max", saMax.CalculateGrossPay(), 23000000m);

            ExpectThrow("T10 Sales rate 0.31", () => new SalesEmployee("X", "X", "Kinh doanh", 8000000m, 1m, 0.31m));
            ExpectThrow("T11 Sales doanh so am", () => new SalesEmployee("X", "X", "Kinh doanh", 8000000m, -1m, 0.1m));

            ExpectThrow("T12 Bonus amount 0", () => new SalariedEmployee("X", "X", 1m).AddBonus(0m));
            ExpectThrow("T13 Bonus amount am", () => new SalariedEmployee("X", "X", 1m).AddBonus(-5m));
            ExpectThrow("T14 Bonus ly do rong", () => new SalariedEmployee("X", "X", 1m).AddBonus(5m, "  "));

            var bRate = new SalariedEmployee("T15", "H", "Ho tro", 10000000m, 0m);
            bRate.AddBonus(0.1m, 1000000m, "Thuong ty le");
            Check("T15 Bonus rate 0.1 x 1M", bRate.MonthlyBonus, 100000m);
            ExpectThrow("T16 Bonus rate 0", () => new SalariedEmployee("X", "X", 1m).AddBonus(0m, 100m, "r"));
            ExpectThrow("T17 Bonus rate 0.51", () => new SalariedEmployee("X", "X", 1m).AddBonus(0.51m, 100m, "r"));

            var p = new Payroll("2026-09");
            p.AddEmployee(new SalariedEmployee("E1", "A", "Ho tro", 100m, 0m));
            ExpectThrow("T18 Trung ma NV", () => p.AddEmployee(new SalariedEmployee("E1", "B", "Ho tro", 100m, 0m)));

            ExpectThrow("T19 Ma rong", () => new SalariedEmployee("", "A", 100m));
            ExpectThrow("T20 Ten rong", () => new SalariedEmployee("X", "  ", 100m));

            var empty = new Payroll("2026-09");
            Check("T21 Payroll rong tong 0", empty.CalculateTotalPayroll(), 0m);

            Console.WriteLine($"Ket qua: {_pass} PASS, {_fail} FAIL");
        }
    }
}

