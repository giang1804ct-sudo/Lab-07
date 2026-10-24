/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Lop phong hoc, giu danh sach thiet bi
// TODO: can xu ly them cho nay...

public class ClassRoom
{
    public string MaPhong { get; set; }
    public string TenPhong { get; set; }
    public int SucChua { get; set; }
    private List<Device> ds = new List<Device>();

    public ClassRoom(string ma, string ten, int sucChua)
    {
        MaPhong = ma;
        TenPhong = ten;
        SucChua = sucChua;
    }

    // Them thiet bi vao phong
    public void AddDevice(Device device)
    {
        // BUG 1: quen kiem tra null, truyen null vao la crash
        // BUG 2: quen kiem tra trung ma, them 2 cai giong nhau cung duoc
        ds.Add(device);
    }

    public bool RemoveDevice(string deviceId)
    {
        Device d = FindDevice(deviceId);
        if (d == null) return false;
        ds.Remove(d);
        return true;
    }

    public Device? FindDevice(string deviceId)
    {
        // vong lap don gian de tim
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].MaThietBi == deviceId)
                return ds[i];
        }
        return null;
    }

    // BUG 3: dung if de kiem tra tung loai, chua dung da hinh
    // Thay bao phai goi TinhPhiBaoTriNam() truc tiep moi dung
    public decimal CalculateAnnualMaintenanceCost()
    {
        decimal tong = 0;
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i] is ProjectorRemote)
                tong = tong + ds[i].GiaMua * 0.1m;
            else if (ds[i] is AirConditionerRemote)
                tong = tong + ds[i].GiaMua * 0.05m;
            else if (ds[i] is WirelessMicro)
                tong = tong + ds[i].GiaMua * 0.15m;
        }
        return tong;
    }

    // BUG 4: moi check UnderMaintenance, quen check thiet bi dung tren 5 nam
    public List<Device> GetDevicesRequiringMaintenance()
    {
        List<Device> kq = new List<Device>();
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].TrangThai == DeviceStatus.UnderMaintenance)
                kq.Add(ds[i]);
        }
        // TODO: thay bao can them dieu kien tren 5 nam nua
        return kq;
    }
}
