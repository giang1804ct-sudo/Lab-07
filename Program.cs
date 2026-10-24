/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== QUẢN LÝ THIẾT BỊ PHÒNG HỌC ===");

            var khoMayChieu = new InMemoryRepository<ProjectorRemote>();
            khoMayChieu.Add(new ProjectorRemote("P01", "DK May chieu 1", "Kho A", 80));
            khoMayChieu.Add(new ProjectorRemote("P02", "DK May chieu 2", "Kho A", 50));
            khoMayChieu.Add(new ProjectorRemote("P03", "DK May chieu 3", "Kho B", 10));

            var khoDieuHoa = new InMemoryRepository<AirConditionerRemote>();
            khoDieuHoa.Add(new AirConditionerRemote("A01", "DK Dieu hoa 1", "Kho A", new List<string> { "A203", "A204" }));
            khoDieuHoa.Add(new AirConditionerRemote("A02", "DK Dieu hoa 2", "Kho B", new List<string> { "B101" }));

            var khoMicro = new InMemoryRepository<WirelessMicrophone>();
            khoMicro.Add(new WirelessMicrophone("M01", "Micro 1", "Kho A", 90, 80));
            khoMicro.Add(new WirelessMicrophone("M02", "Micro 2", "Kho A", 50, 30));
            khoMicro.Add(new WirelessMicrophone("M03", "Micro 3", "Kho B", 70, 60));

            // 1. Thêm + 2. Em test thử trùng mã xem có chặn không
            Console.WriteLine("\n1-2. Test trùng mã:");
            try
            {
                khoMayChieu.Add(new ProjectorRemote("P01", "Trung ma", "Kho A", 60));
                Console.WriteLine("Lỗi: không thể chặn trùng mã"); 
            }
            catch (DuplicateIdException ex)
            {
                Console.WriteLine("Chặn trùng mã OK: " + ex.Message); 
            }

            // 3. Tìm theo mã
            Console.WriteLine("\n3. Tim P02: " + khoMayChieu.FindById("P02").Name);

            // 4. Lọc sẵn sàng
            Console.WriteLine("\n4. Máy chiếu sẵn sàng:");
            var sanSang = khoMayChieu.FindAll(d => d.IsReadyForUse());
            foreach (var d in sanSang) Console.WriteLine("- " + d.Id + " pin=" + d.BatteryLevel);

            // 5. Chọn máy chiếu điểm cao nhất
            var s1 = new AllocationService<ProjectorRemote>(
                khoMayChieu, new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore()));
            var p = s1.Allocate(d => true);
            Console.WriteLine("\n5. Chọn máy chiếu tốt nhất: " + p.Id);

            // 6. Chọn điều hòa hợp phòng A203 
            var s2 = new AllocationService<AirConditionerRemote>(
                khoDieuHoa, new FirstAvailableStrategy<AirConditionerRemote>());
            var dh = s2.Allocate(d => d.IsCompatibleWith("a203"));
            Console.WriteLine("6. Điều hòa hợp phòng A203: " + dh.Id);

            // 7. Chọn micro đầu tiên sẵn sàng (M02 song yếu sẽ bị loại)
            var s3 = new AllocationService<WirelessMicrophone>(
                khoMicro, new FirstAvailableStrategy<WirelessMicrophone>());
            var mic = s3.Allocate(d => true);
            Console.WriteLine("7. Micro đầu tiên sẵn sàng: " + mic.Id);

            // 8. Đổi chiến lược lúc chạy
            s1.ChangeStrategy(new FirstAvailableStrategy<ProjectorRemote>());
            var p2 = s1.Allocate(d => true);
            Console.WriteLine("8. Đổi strategy, chon: " + p2.Id);

            // 9. Cấp phát khi không còn viên -> báo lỗi
            Console.WriteLine("\n9. Test hết hàng:");
            try { s1.Allocate(d => true); }
            catch (NoSuitableItemException ex) { Console.WriteLine("Bao loi OK: " + ex.Message); }

            // 10. Kiểm tra Borrowed
            Console.WriteLine("\n10. Trang thai " + p.Id + " = " + p.Status);

            // 11. Repo dùng được nhiều loại mẫ (generic)
            Console.WriteLine("11. Repo generic dùng chung cho 3 lọai OK.");

            Console.WriteLine("\nXong test ban 3.");
        }
    }
}
