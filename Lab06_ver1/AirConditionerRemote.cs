/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Dieu khien dieu hoa

public class AirConditionerRemote : TeachingDevice
{
    public AirConditionerRemote(string ma, string ten, int nam, decimal gia)
        : base(ma, ten, nam, gia, 3, 10000) // toi da 3 ngay
    {
    }

    // Phi bao tri = 5% gia mua
    public override decimal TinhPhiBaoTriNam()
    {
        return GiaMua * 0.05m;
    }
}
