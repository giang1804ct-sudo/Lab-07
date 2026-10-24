/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Dieu khien may chieu

public class ProjectorRemote : TeachingDevice
{
    public ProjectorRemote(string ma, string ten, int nam, decimal gia)
        : base(ma, ten, nam, gia, 7, 20000) // duoc muon toi da 7 ngay
    {
    }

    // Phi bao tri = 10% gia mua
    public override decimal TinhPhiBaoTriNam()
    {
        return GiaMua * 0.1m;
    }
}
