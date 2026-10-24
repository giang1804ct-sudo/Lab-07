// Printer + NetworkPrinter (phuong an mang cho "chi mot so may in")
using System;

public enum PrinterKind { Laser, Inkjet }

public class Printer : Device
{
    public PrinterKind Kind { get; set; }
    public long PrintedPages { get; set; }
    public bool SupportsNetwork { get; set; }
    // Bo sung IsColor vi de yeu cau "+300k neu la may in mau"
    // nhung khong cho thuoc tinh luu mau/don sac.
    public bool IsColor { get; set; }

    public Printer(string id, string name, int year, decimal price,
        PrinterKind kind, long printedPages, bool supportsNetwork, bool isColor,
        DeviceStatus status = DeviceStatus.Active)
        : base(id, name, year, price, status)
    { Kind = kind; PrintedPages = printedPages; SupportsNetwork = supportsNetwork; IsColor = isColor; }

    // Bao tri: 4% gia mua + 500k neu >100k trang + 300k neu in mau.
    public override decimal CalculateAnnualMaintenanceCost()
    {
        decimal cost = PurchasePrice * 0.04m;
        if (PrintedPages > 100_000) cost += 500_000m;
        if (IsColor) cost += 300_000m;
        return cost;
    }

    public override string ToString()
        => base.ToString() + $" | Loai: {Kind} | Da in: {PrintedPages:N0} | Mang: {(SupportsNetwork ? "Co" : "Khong")} | Mau: {(IsColor ? "Co" : "Khong")}";
}

// Phuong an duoc chon: NetworkPrinter ke thua Printer + INetworkable.
// Giu Printer thuong nhe; chi may co mang moi co Connect/Disconnect.
// Trade-off: them 1 lop, phai check (is/as INetworkable); bu lai dung
// nghiep vu "chi mot so may in co mang", tai su dung Printer, de mo rong.
public class NetworkPrinter : Printer, INetworkable
{
    public string IpAddress { get; private set; } = string.Empty;
    public bool IsConnected { get; private set; }

    public NetworkPrinter(string id, string name, int year, decimal price,
        PrinterKind kind, long printedPages, bool isColor,
        DeviceStatus status = DeviceStatus.Active)
        : base(id, name, year, price, kind, printedPages, true, isColor, status) { }

    public void Connect(string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentException("Dia chi IP khong duoc rong.", nameof(ipAddress));
        if (IsConnected)
            throw new InvalidOperationException($"Thiet bi {DeviceId} dang ket noi, khong cho ket noi lai.");
        IpAddress = ipAddress.Trim();
        IsConnected = true;
    }

    public void Disconnect()
    {
        IsConnected = false;
        IpAddress = string.Empty;
    }

    public override string ToString() => base.ToString() + " | (NetworkPrinter)";
}
