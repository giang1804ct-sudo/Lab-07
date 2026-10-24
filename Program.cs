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
            // Yêu cầu thêm dấu cho bình luận
            Console.WriteLine("QUẢN LÝ THIẾT BỊ PHÒNG DẠY ");

            // Tạo kho cho từng loại
            var khoMayChieu = new InMemoryRepository<ProjectorRemote>();
            khoMayChieu.Add(new ProjectorRemote("P01", "DK May chieu 1", "Kho A", 80));
            khoMayChieu.Add(new ProjectorRemote("P02", "DK May chieu 2", "Kho A", 50));
            khoMayChieu.Add(new ProjectorRemote("P03", "DK May chieu 3", "Kho B", 10)); // pin yeu

            var khoDieuHoa = new InMemoryRepository<AirConditionerRemote>();
            khoDieuHoa.Add(new AirConditionerRemote("A01", "DK Dieu hoa 1", "Kho A", new List<string> { "A203", "A204" }));
            khoDieuHoa.Add(new AirConditionerRemote("A02", "DK Dieu hoa 2", "Kho B", new List<string> { "B101" }));

            var khoMicro = new InMemoryRepository<WirelessMicrophone>();
            khoMicro.Add(new WirelessMicrophone("M01", "Micro 1", "Kho A", 90, 80));
            khoMicro.Add(new WirelessMicrophone("M02", "Micro 2", "Kho A", 50, 30)); // tín hiệu yếu
            khoMicro.Add(new WirelessMicrophone("M03", "Micro 3", "Kho B", 70, 60));

            // Test 1: Tìm theo mã, em test thử 1 cái đầu
            Console.WriteLine("\n-- Test tìm theo mã P01 --");
            var timThay = khoMayChieu.FindById("P01"); // em đổi tên found -> timThay
            if (timThay != null)
            {
                Console.WriteLine("Tim thay: " + timThay.Name + ", pin=" + timThay.BatteryLevel);
            }
            Console.WriteLine("Tổng số máy chieu: " + khoMayChieu.GetAll().Count); // em in thêm cho đếm

            // Test 2: Chọn máy chiếu điểm cao nhất
            var cachChon = new HighestPriorityStrategy<ProjectorRemote>(
                device => device.GetDefaultPriorityScore());
            var dichVu = new AllocationService<ProjectorRemote>(khoMayChieu, cachChon);
            var selected = dichVu.Allocate(device => true);
            Console.WriteLine("\n-- Đã cấp phạt: " + selected.Id + " pin=" + selected.BatteryLevel);

            // Test 3: Chọn điều hòa phù hợp phòng A203
            var dichVu2 = new AllocationService<AirConditionerRemote>(
                khoDieuHoa, new FirstAvailableStrategy<AirConditionerRemote>());
            var dh = dichVu2.Allocate(device => device.IsCompatibleWith("A203"));
            Console.WriteLine("Đã cấp phạt điều hòa: " + dh.Id);

            // Test 4: Chọn micro đầu tiên, em lấy cái đầu tiên cho nhanh
            var dichVu3 = new AllocationService<WirelessMicrophone>(
                khoMicro, new FirstAvailableStrategy<WirelessMicrophone>());
            var mic = dichVu3.Allocate(device => true);
            Console.WriteLine("Đã cấp phạt micro: " + mic.Id);

            Console.WriteLine("\nXong");
        }
    }
}
