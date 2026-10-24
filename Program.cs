using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    /***************************************/
    //MSSV: 202418889
    //Họ tên: Đặng Thị Hương Giang 
    /***************************************/
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LAB 07: QUẢN LÝ THIẾT BỊ PHÒNG HỌC ===");

            // 1. Thêm thiết bị vào repository
            Console.WriteLine("\n[Test 1] Thêm thiết bị vào repository:");

            var khoMayChieu = new InMemoryRepository<ProjectorRemote>();
            khoMayChieu.Add(new ProjectorRemote("P01", "DK May chieu 1", "Kho A", 80));
            khoMayChieu.Add(new ProjectorRemote("P02", "DK May chieu 2", "Kho A", 50));
            khoMayChieu.Add(new ProjectorRemote("P03", "DK May chieu 3", "Kho B", 10)); // Pin yếu
            Console.WriteLine("-> Đã thêm 3 máy chiếu thành công.");

            var khoDieuHoa = new InMemoryRepository<AirConditionerRemote>();
            khoDieuHoa.Add(new AirConditionerRemote("A01", "DK Dieu hoa 1", "Kho A", new List<string> { "A203", "A204" }));
            khoDieuHoa.Add(new AirConditionerRemote("A02", "DK Dieu hoa 2", "Kho B", new List<string> { "B101" }));
            Console.WriteLine("-> Đã thêm 2 điều hòa thành công.");

            var khoMicro = new InMemoryRepository<WirelessMicrophone>();
            khoMicro.Add(new WirelessMicrophone("M01", "Micro 1", "Kho A", 90, 80));
            khoMicro.Add(new WirelessMicrophone("M02", "Micro 2", "Kho A", 50, 30)); // Tín hiệu kém
            khoMicro.Add(new WirelessMicrophone("M03", "Micro 3", "Kho B", 70, 60));
            Console.WriteLine("-> Đã thêm 3 micro thành công.");

            // 2. Thử thêm hai thiết bị trùng mã
            Console.WriteLine("\n[Test 2] Thử thêm thiết bị trùng mã 'P01':");
            try
            {
                khoMayChieu.Add(new ProjectorRemote("P01", "Máy chiếu fake", "Kho C", 100));
                Console.WriteLine("-> LỖI: Hệ thống không chặn được trùng mã!"); 
            }
            catch (DuplicateIdException ex)
            {
                Console.WriteLine($"-> THÀNH CÔNG: Bắt được lỗi - {ex.Message}"); 
            }

            // 3. Tìm thiết bị theo mã
            Console.WriteLine("\n[Test 3] Tìm thiết bị mã 'P02':");
            var foundDevice = khoMayChieu.FindById("P02");
            Console.WriteLine($"-> Tìm thấy: {foundDevice?.Name} - Pin: {foundDevice?.BatteryLevel}%");

            // 4. Lọc các thiết bị sẵn sàng
            Console.WriteLine("\n[Test 4] Danh sách Micro sẵn sàng (Pin >= 30, Tín hiệu >= 40):");
            var readyMics = khoMicro.FindAll(d => d.IsReadyForUse());
            foreach (var d in readyMics)
            {
                Console.WriteLine($"-> {d.Id}: Pin {d.BatteryLevel}%, Tín hiệu {d.SignalQuality}%");
            }

            // 5. Chọn điều khiển máy chiếu có điểm cao nhất
            Console.WriteLine("\n[Test 5] Cấp phát máy chiếu có điểm (pin) cao nhất:");
            var s1 = new AllocationService<ProjectorRemote>(
                khoMayChieu, new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore()));
            var bestProjector = s1.Allocate(d => true);
            Console.WriteLine($"-> Đã cấp phát: {bestProjector.Id} (Pin: {bestProjector.BatteryLevel}%)");

            // 6. Chọn điều khiển điều hòa tương thích với một phòng
            Console.WriteLine("\n[Test 6] Cấp phát điều hòa cho phòng A203:");
            var s2 = new AllocationService<AirConditionerRemote>(
                khoDieuHoa, new FirstAvailableStrategy<AirConditionerRemote>());
            var ac = s2.Allocate(d => d.IsCompatibleWith("A203")); // Sửa thành A hoa để khớp dữ liệu cứng
            Console.WriteLine($"-> Đã cấp phát: {ac.Id}");

            // 7. Chọn micro đầu tiên sẵn sàng
            Console.WriteLine("\n[Test 7] Cấp phát micro đầu tiên sẵn sàng:");
            var s3 = new AllocationService<WirelessMicrophone>(
                khoMicro, new FirstAvailableStrategy<WirelessMicrophone>());
            var firstReadyMic = s3.Allocate(d => true);
            Console.WriteLine($"-> Đã cấp phát: {firstReadyMic.Id}");

            // 8. Đổi chiến lược trong thời gian chạy và quan sát kết quả
            Console.WriteLine("\n[Test 8] Đổi chiến lược máy chiếu sang 'First Available':");
            s1.ChangeStrategy(new FirstAvailableStrategy<ProjectorRemote>());
            var nextProjector = s1.Allocate(d => true);
            Console.WriteLine($"-> Đã cấp phát: {nextProjector.Id} (Pin: {nextProjector.BatteryLevel}%) - Chú ý: P01 đã mượn nên sẽ lấy P02.");

            // 9. Thử cấp phát khi không có ứng viên phù hợp
            Console.WriteLine("\n[Test 9] Thử cấp phát điều hòa cho phòng X999 (không tồn tại):");
            try 
            { 
                s2.Allocate(d => d.IsCompatibleWith("X999")); 
            }
            catch (NoSuitableItemException ex) 
            { 
                Console.WriteLine($"-> THÀNH CÔNG: Bắt được lỗi - {ex.Message}"); 
            }

            // 10. Chứng minh thiết bị được chọn chuyển sang Borrowed
            Console.WriteLine("\n[Test 10] Kiểm tra trạng thái của thiết bị sau khi cấp phát:");
            Console.WriteLine($"-> Trạng thái của {bestProjector.Id}: {bestProjector.Status}");
            
            // 11. Chứng minh repository tổng quát dùng được cho nhiều loại
            Console.WriteLine("\n[Test 11] Tính tổng quát của hệ thống:");
            Console.WriteLine($"-> khoMayChieu chứa: {khoMayChieu.GetAll().Count} thiết bị.");
            Console.WriteLine($"-> khoDieuHoa chứa: {khoDieuHoa.GetAll().Count} thiết bị.");
            Console.WriteLine($"-> khoMicro chứa: {khoMicro.GetAll().Count} thiết bị.");
            Console.WriteLine("-> KIỂM CHỨNG: Cả 3 kho đều dùng chung logic từ InMemoryRepository<T> mà không phải viết lại code lưu trữ.");
            
            Console.ReadLine();
        }
    }
}