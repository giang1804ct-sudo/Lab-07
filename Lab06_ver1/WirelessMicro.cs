/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************>
// Micro khong day

public class WirelessMicro : TeachingDevice
{
    public WirelessMicro(string ma, string ten, int nam, decimal gia)
        : base(ma, ten, nam, gia, 1, 50000) // chi duoc muon 1 ngay
    {
    }

    // Phi bao tri = 15% gia mua
    public override decimal TinhPhiBaoTriNam()
    {
        return GiaMua * 0.15m;
    }
}
