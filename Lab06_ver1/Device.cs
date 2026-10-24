/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Lop tru tuong Device, dung chung cho moi thiet bi
// Gom cac thong tin co ban: ma, ten, nam su dung, gia, trang thai

public abstract class Device
{
    // Ma chi cho set 1 lan khi khoi tao
    public string MaThietBi { get; }
    public string TenThietBi { get; set; }
    public int NamSuDung { get; set; }
    public decimal GiaMua { get; set; }
    public DeviceStatus TrangThai { get; set; }

    // Ham khoi tao, co kiem tra dau vao don gian
    public Device(string ma, string ten, int nam, decimal gia)
    {
        if (string.IsNullOrWhiteSpace(ma))
            throw new ArgumentException("Ma thiet bi khong duoc rong");

        if (gia <= 0)
            throw new ArgumentException("Gia mua phai lon hon 0");

        if (nam > DateTime.Now.Year)
            throw new ArgumentException("Nam su dung khong duoc lon hon nam hien tai");

        MaThietBi = ma;
        TenThietBi = ten;
        NamSuDung = nam;
        GiaMua = gia;
        TrangThai = DeviceStatus.Available; // mac dinh la san sang
    }

    // So nam da dung, de xet bao tri
    public int SoNamDaDung()
    {
        return DateTime.Now.Year - NamSuDung;
    }

    // Ham tru tuong, lop con tu tinh phi bao tri
    public abstract decimal TinhPhiBaoTriNam();

    // In thong tin ra chuoi
    public override string ToString()
    {
        return MaThietBi + " - " + TenThietBi + " - " + TrangThai + " - Gia: " + GiaMua;
    }
}
