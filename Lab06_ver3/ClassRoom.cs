/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
Lab 06 ver3 - ban da sua loi tu ver1
****************/
// Lop phong hoc, giu danh sach thiet bi
// Da fix 4 loi cua ver1

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

    // FIX 1: them check null, tranh crash khi truyen null vao
    // FIX 2: check trung ma, khong cho them 2 thiet bi giong nhau
    public void AddDevice(Device device)
    {
        if (device == null)
            throw new ArgumentException("Thiet bi khong duoc null");

        if (FindDevice(device.MaThietBi) != null)
            throw new DuplicateDeviceException("Trung ma thiet bi: " + device.MaThietBi);

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
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].MaThietBi == deviceId)
                return ds[i];
        }
        return null;
    }

    // FIX 3: dung da hinh, goi truc tiep TinhPhiBaoTriNam()
    // Ly do: neu them loai moi thi khong phai sua if-else nua
    public decimal CalculateAnnualMaintenanceCost()
    {
        decimal tong = 0;
        for (int i = 0; i < ds.Count; i++)
        {
            tong = tong + ds[i].TinhPhiBaoTriNam();
        }
        return tong;
    }

    // FIX 4: them dieu kien dung tren 5 nam
    // Ly do: de dung yeu cau, thiet bi cu qua cung can bao tri
    public List<Device> GetDevicesRequiringMaintenance()
    {
        List<Device> kq = new List<Device>();
        for (int i = 0; i < ds.Count; i++)
        {
            if (ds[i].TrangThai == DeviceStatus.UnderMaintenance || ds[i].SoNamDaDung() > 5)
                kq.Add(ds[i]);
        }
        return kq;
    }
}
