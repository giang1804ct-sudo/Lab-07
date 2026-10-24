// Computer: thuc thi INetworkable (mac dinh co mang)
using System;

public class Computer : Device, INetworkable
{
    public int RamGB { get; set; }
    public string CpuType { get; set; }
    public bool HasDiscreteGpu { get; set; }
    public string IpAddress { get; private set; } = string.Empty;
    public bool IsConnected { get; private set; }

    public Computer(string id, string name, int year, decimal price,
        int ramGB, string cpuType, bool hasGpu,
        DeviceStatus status = DeviceStatus.Active)
        : base(id, name, year, price, status)
    { RamGB = ramGB; CpuType = cpuType; HasDiscreteGpu = hasGpu; }

    // Bao tri: 5% gia mua + 2% neu co GPU roi + 1% neu dung tren 5 nam.
    public override decimal CalculateAnnualMaintenanceCost()
    {
        decimal cost = PurchasePrice * 0.05m;
        if (HasDiscreteGpu) cost += PurchasePrice * 0.02m;
        if (YearsUsed > 5) cost += PurchasePrice * 0.01m;
        return cost;
    }

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

    public override string ToString()
        => base.ToString() + $" | RAM: {RamGB}GB | CPU: {CpuType} | GPU roi: {(HasDiscreteGpu ? "Co" : "Khong")}";
}
