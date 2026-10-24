// 6. Kiem thu toi thieu
using System;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var r1 = new LabRoom("P201", "Phong thuc hanh 201", 40);
        var r2 = new LabRoom("P202", "Phong thuc hanh 202", 30);

        var pc1 = new Computer("PC01", "May tinh 01", 2021, 20_000_000m, 16, "Core i7", true);
        var pc2 = new Computer("PC02", "May tinh 02", 2018, 15_000_000m, 8, "Core i5", false);
        var pr1 = new Printer("PR01", "May in laser", 2020, 10_000_000m, PrinterKind.Laser, 120_000, false, false);
        var pr2 = new NetworkPrinter("PR02", "May in mau mang", 2022, 12_000_000m, PrinterKind.Inkjet, 50_000, true);
        var pj1 = new Projector("PJ01", "May chieu Epson", 2019, 30_000_000m, 4000, 3500, DeviceStatus.UnderMaintenance);

        r1.AddDevice(pc1); r1.AddDevice(pr1); r1.AddDevice(pj1);
        r2.AddDevice(pc2); r2.AddDevice(pr2);

        Console.WriteLine("== 2. Thu them TRUNG MA ==");
        try { r1.AddDevice(new Computer("PC01", "May nhai", 2023, 18_000_000m, 16, "Ryzen 5", false)); }
        catch (Exception ex) { Console.WriteLine($"  Loi du kien: {ex.Message}"); }

        Console.WriteLine("\n== 3. Danh sach tung phong ==");
        foreach (var r in new[] { r1, r2 })
        {
            Console.WriteLine($"-- {r.RoomId} - {r.RoomName} ({r.Devices.Count}):");
            foreach (var d in r.Devices) Console.WriteLine($"   {d}");
        }

        Console.WriteLine("\n== 4. Tong bao tri moi phong ==");
        foreach (var r in new[] { r1, r2 })
            Console.WriteLine($"   {r.RoomId}: {r.CalculateAnnualMaintenanceCost():N0} dong/nam");

        Console.WriteLine("\n== 5. Can bao tri ==");
        foreach (var r in new[] { r1, r2 })
            foreach (var d in r.GetDevicesRequiringMaintenance())
                Console.WriteLine($"   {r.RoomId}: {d}");

        Console.WriteLine("\n== 6. Ket noi mang ==");
        var all = r1.Devices.Concat(r2.Devices).ToList();
        int n = 10;
        foreach (var d in all)
            if (d is INetworkable net) { net.Connect($"192.168.1.{n}"); n += 10; Console.WriteLine($"   {d.DeviceId} -> {net.IpAddress}, Connected={net.IsConnected}"); }
            else Console.WriteLine($"   {d.DeviceId} khong ho tro mang.");

        Console.WriteLine("\n== Dieu kien noi ==");
        try { ((INetworkable)pc1).Connect("   "); }
        catch (Exception ex) { Console.WriteLine($"  IP rong -> {ex.Message}"); }
        try { ((INetworkable)pc1).Connect("192.168.1.99"); }
        catch (Exception ex) { Console.WriteLine($"  Ket noi lai -> {ex.Message}"); }

        Console.WriteLine("\n== 7. Duyet qua INetworkable ==");
        List<INetworkable> mang = all.OfType<INetworkable>().ToList();
        foreach (var m in mang) Console.WriteLine($"   {m.GetType().Name} IP={m.IpAddress} Connected={m.IsConnected}");
        foreach (var m in mang) m.Disconnect();
        Console.WriteLine("   Sau Disconnect:");
        foreach (var m in mang) Console.WriteLine($"   {m.GetType().Name} IP='{m.IpAddress}' Connected={m.IsConnected}");

        Console.WriteLine("\nHoan tat kiem thu toi thieu.");
    }
}
