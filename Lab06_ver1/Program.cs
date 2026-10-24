/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Chuong trinh chinh de test

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== LAB 06 ver1 - ban nhap, con loi ===");

        // 11.1 Luong thanh cong
        try
        {
            Lecturer gv = new Lecturer("GV01", "Thay A");
            ProjectorRemote dkMayChieu = new ProjectorRemote("TB01", "DK May chieu", 2022, 500000);
            AirConditionerRemote dkDieuHoa = new AirConditionerRemote("TB02", "DK Dieu hoa", 2021, 300000);
            WirelessMicro micro = new WirelessMicro("TB03", "Micro", 2023, 1000000);

            ClassRoom phong = new ClassRoom("P101", "Phong 101", 50);
            phong.AddDevice(dkMayChieu);
            phong.AddDevice(dkDieuHoa);
            phong.AddDevice(micro);
            Console.WriteLine("Tien bao tri 1 nam: " + phong.CalculateAnnualMaintenanceCost());

            BorrowingSlip phieu = new BorrowingSlip("PH01", gv, "P101", DateTime.Now);
            phieu.AddDevice(dkMayChieu, 5);
            phieu.AddDevice(micro, 1);
            Console.WriteLine("Them 2 thiet bi ok");

            decimal tien = phieu.ReturnDevice("TB03", DateTime.Now.AddDays(2));
            Console.WriteLine("Tra micro tre 2 ngay, tien phat: " + tien);

            Console.WriteLine("Tong tien tre: " + phieu.CalculateTotalLateFee());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Loi: " + ex.Message);
        }

        // 11.2 Luong ngoai le (test may tinh huong)
        Console.WriteLine("\n--- Test ngoai le ---");
        try
        {
            TeachingAssistant tg = new TeachingAssistant("TG01", "Tro giang B");
            AirConditionerRemote dkHong = new AirConditionerRemote("TB10", "DK hong", 2020, 200000);
            dkHong.TrangThai = DeviceStatus.UnderMaintenance;

            BorrowingSlip p2 = new BorrowingSlip("PH02", tg, "P102", DateTime.Now);
            // cho muon thiet bi hong -> dang le phai bao loi nhung ver1 chua check nen van cho muon
            p2.AddDevice(dkHong, 2);
            Console.WriteLine("BUG: van cho muon thiet bi hong, dang le phai nem DeviceUnavailableException");

            // them trung 2 lan
            WirelessMicro m2 = new WirelessMicro("TB11", "Micro 2", 2022, 800000);
            p2.AddDevice(m2, 1);
            p2.AddDevice(m2, 1); // dang le phai bao DuplicateDeviceException
            Console.WriteLine("BUG: them trung ma van duoc");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Bat duoc ngoai le: " + ex.GetType().Name + " - " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Xong phan test (finally luon chay).");
        }
    }
}
