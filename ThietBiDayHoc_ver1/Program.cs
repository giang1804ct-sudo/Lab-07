/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== QUAN LY THIET BI PHONG DAY (ban 1 - con loi) ===");

            // Tao kho cho tung loai
            var khoMayChieu = new InMemoryRepository<ProjectorRemote>();
            khoMayChieu.Add(new ProjectorRemote("P01", "DK May chieu 1", "Kho A", 80));
            khoMayChieu.Add(new ProjectorRemote("P02", "DK May chieu 2", "Kho A", 50));
            khoMayChieu.Add(new ProjectorRemote("P03", "DK May chieu 3", "Kho B", 10)); // pin yeu

            var khoDieuHoa = new InMemoryRepository<AirConditionerRemote>();
            khoDieuHoa.Add(new AirConditionerRemote("A01", "DK Dieu hoa 1", "Kho A", new List<string> { "A203", "A204" }));
            khoDieuHoa.Add(new AirConditionerRemote("A02", "DK Dieu hoa 2", "Kho B", new List<string> { "B101" }));

            var khoMicro = new InMemoryRepository<WirelessMicrophone>();
            khoMicro.Add(new WirelessMicrophone("M01", "Micro 1", "Kho A", 90, 80));
            khoMicro.Add(new WirelessMicrophone("M02", "Micro 2", "Kho A", 50, 30)); // tin hieu yeu
            khoMicro.Add(new WirelessMicrophone("M03", "Micro 3", "Kho B", 70, 60));

            // Test 1: tim theo ma, em test thu 1 cai truoc
            Console.WriteLine("\n-- Test tim theo ma P01 --");
            var timThay = khoMayChieu.FindById("P01"); // em doi ten found -> timThay
            if (timThay != null)
            {
                Console.WriteLine("Tim thay: " + timThay.Name + ", pin=" + timThay.BatteryLevel);
            }
            Console.WriteLine("Tong so may chieu: " + khoMayChieu.GetAll().Count); // em in them cho de dem

            // Test 2: chon may chieu diem cao nhat
            var cachChon = new HighestPriorityStrategy<ProjectorRemote>(
                device => device.GetDefaultPriorityScore());
            var dichVu = new AllocationService<ProjectorRemote>(khoMayChieu, cachChon);
            var selected = dichVu.Allocate(device => true);
            Console.WriteLine("\n-- Da cap phat: " + selected.Id + " pin=" + selected.BatteryLevel);

            // Test 3: chon dieu hoa hop phong A203
            var dichVu2 = new AllocationService<AirConditionerRemote>(
                khoDieuHoa, new FirstAvailableStrategy<AirConditionerRemote>());
            var dh = dichVu2.Allocate(device => device.IsCompatibleWith("A203"));
            Console.WriteLine("Da cap phat dieu hoa: " + dh.Id);

            // Test 4: chon micro dau tien, em lay cai dau tien cho nhanh
            var dichVu3 = new AllocationService<WirelessMicrophone>(
                khoMicro, new FirstAvailableStrategy<WirelessMicrophone>());
            var mic = dichVu3.Allocate(device => true);
            Console.WriteLine("Da cap phat micro: " + mic.Id);

            Console.WriteLine("\nXong. Con mot so loi can sua o ban sau.");
        }
    }
}
