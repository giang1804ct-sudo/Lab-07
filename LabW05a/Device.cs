// Enum trang thai + Interface INetworkable + Lop truu tuong Device
using System;

/// <summary>Trang thai hoat dong cua thiet bi.</summary>
public enum DeviceStatus { Active, UnderMaintenance, Retired }

/// <summary>
/// Kha nang ket noi mang (cat ngang cay ke thua).
/// Dung interface vi kha nang mang KHONG phu thuoc hoan toan vao loai
/// thiet bi: mot so Computer/Printer co mang, mot so khong.
/// </summary>
public interface INetworkable
{
    string IpAddress { get; }
    void Connect(string ipAddress);
    void Disconnect();
    bool IsConnected { get; }
}

/// <summary>
/// Lop cha truu tuong: thong tin chung + hanh vi chung.
/// La abstract vi khong co "thiet bi chung chung" ngoai doi thuc,
/// va cach tinh bao tri phu thuoc loai thiet bi (de abstract).
/// </summary>
public abstract class Device
{
    public string DeviceId { get; }
    public string DeviceName { get; set; }
    public int YearInUse { get; }
    public decimal PurchasePrice { get; }
    public DeviceStatus Status { get; set; }

    protected Device(string deviceId, string deviceName, int yearInUse,
        decimal purchasePrice, DeviceStatus status = DeviceStatus.Active)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
            throw new ArgumentException("Ma thiet bi khong duoc rong.", nameof(deviceId));
        if (purchasePrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(purchasePrice), "Gia mua phai lon hon 0.");
        if (yearInUse > DateTime.Now.Year)
            throw new ArgumentException("Nam dua vao SD khong duoc lon hon nam hien tai.", nameof(yearInUse));
        DeviceId = deviceId.Trim();
        DeviceName = deviceName;
        YearInUse = yearInUse;
        PurchasePrice = purchasePrice;
        Status = status;
    }

    public int YearsUsed => DateTime.Now.Year - YearInUse;

    /// <summary>Tinh chi phi bao tri 1 nam (moi loai 1 cong thuc -> da hinh).</summary>
    public abstract decimal CalculateAnnualMaintenanceCost();

    public override string ToString()
        => $"[{GetType().Name}] {DeviceId} - {DeviceName} | Nam SD: {YearInUse} | Gia: {PurchasePrice:N0}d | TT: {Status}";
}
