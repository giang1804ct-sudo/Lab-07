/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Lop TeachingDevice ke thua Device
// Them 2 thuoc tinh: so ngay muon toi da + phi tre 1 ngay
// De xu ly muon/tra trong phieu

public abstract class TeachingDevice : Device
{
    public int MaxBorrowDays { get; set; } // so ngay duoc muon toi da
    public decimal LateFeePerDay { get; set; } // tien phat 1 ngay tre

    public TeachingDevice(string ma, string ten, int nam, decimal gia, int maxDays, decimal fee)
        : base(ma, ten, nam, gia)
    {
        MaxBorrowDays = maxDays;
        LateFeePerDay = fee;
    }

    // Doi trang thai sang Borrowed
    public void MarkAsBorrowed()
    {
        TrangThai = DeviceStatus.Borrowed;
    }

    // Tra xong thi ve Available
    public void MarkAsAvailable()
    {
        TrangThai = DeviceStatus.Available;
    }

    // Tinh tien tre
    public decimal TinhTienTre(int soNgayTre)
    {
        if (soNgayTre <= 0) return 0;
        return soNgayTre * LateFeePerDay;
    }
}
