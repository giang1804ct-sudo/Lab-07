/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Chuong trinh test day du theo muc 9 cua de (ban 3)
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== QUAN LY THIET BI PHONG DAY (ban 3 - da sua) ===");

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

            // 1. Them + 2. Em test thu trung ma xem co chan khong
            Console.WriteLine("\n1-2. Test trung ma:");
            try
            {
                khoMayChieu.Add(new ProjectorRemote("P01", "Trung ma", "Kho A", 60));
                Console.WriteLine("Loi: khong chan duoc trung ma"); // neu chay toi day la sai
            }
            catch (DuplicateIdException ex)
            {
                Console.WriteLine("Chan trung ma OK: " + ex.Message); // em thay bao vay la duoc
            }

            // 3. Tim theo ma
            Console.WriteLine("\n3. Tim P02: " + khoMayChieu.FindById("P02").Name);

            // 4. Loc san sang
            Console.WriteLine("\n4. May chieu san sang:");
            var sanSang = khoMayChieu.FindAll(d => d.IsReadyForUse());
            foreach (var d in sanSang) Console.WriteLine("- " + d.Id + " pin=" + d.BatteryLevel);

            // 5. Chon may chieu diem cao nhat
            var s1 = new AllocationService<ProjectorRemote>(
                khoMayChieu, new HighestPriorityStrategy<ProjectorRemote>(d => d.GetDefaultPriorityScore()));
            var p = s1.Allocate(d => true);
            Console.WriteLine("\n5. Chon may chieu tot nhat: " + p.Id);

            // 6. Chon dieu hoa hop phong A203 (thu viet thuong a203 van duoc)
            var s2 = new AllocationService<AirConditionerRemote>(
                khoDieuHoa, new FirstAvailableStrategy<AirConditionerRemote>());
            var dh = s2.Allocate(d => d.IsCompatibleWith("a203"));
            Console.WriteLine("6. Dieu hoa hop A203: " + dh.Id);

            // 7. Chon micro dau tien san sang (M02 song yeu se bi loai)
            var s3 = new AllocationService<WirelessMicrophone>(
                khoMicro, new FirstAvailableStrategy<WirelessMicrophone>());
            var mic = s3.Allocate(d => true);
            Console.WriteLine("7. Micro dau tien san sang: " + mic.Id);

            // 8. Doi chien luoc luc chay
            s1.ChangeStrategy(new FirstAvailableStrategy<ProjectorRemote>());
            var p2 = s1.Allocate(d => true);
            Console.WriteLine("8. Doi strategy, chon: " + p2.Id);

            // 9. Cap phat khi khong con vien -> bao loi
            Console.WriteLine("\n9. Test het hang:");
            try { s1.Allocate(d => true); }
            catch (NoSuitableItemException ex) { Console.WriteLine("Bao loi OK: " + ex.Message); }

            // 10. Kiem tra Borrowed
            Console.WriteLine("\n10. Trang thai " + p.Id + " = " + p.Status);

            // 11. Repo dung duoc nhieu loai ma khong copy code (generic)
            Console.WriteLine("11. Repo generic dung chung cho 3 loai OK.");

            Console.WriteLine("\nXong test ban 3.");
        }
    }
}
