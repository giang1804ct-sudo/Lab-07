/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
Lab 06 ver3 - da sua cach AddDevice cho nhat quan
****************/

public class BorrowingSlip
{
    public string SlipId { get; set; }
    public IBorrower Borrower { get; set; }
    public string TeachingRoom { get; set; }
    public DateTime BorrowedDate { get; set; }

    private List<BorrowItem> items = new List<BorrowItem>();
    public IReadOnlyList<BorrowItem> Items
    {
        get { return items; }
    }

    public BorrowingSlip(string id, IBorrower borrower, string room, DateTime date)
    {
        SlipId = id;
        Borrower = borrower;
        TeachingRoom = room;
        BorrowedDate = date;
    }

    // Ham kiem tra rieng, de code gon hon
    private void ValidateBorrowing(TeachingDevice device, int borrowDays)
    {
        if (device == null)
            throw new ArgumentException("Device khong duoc null");

        if (borrowDays <= 0)
            throw new InvalidBorrowPeriodException(device.MaThietBi, borrowDays, device.MaxBorrowDays);

        if (borrowDays > device.MaxBorrowDays)
            throw new InvalidBorrowPeriodException(device.MaThietBi, borrowDays, device.MaxBorrowDays);

        // check trung trong phieu
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Device.MaThietBi == device.MaThietBi)
                throw new DuplicateDeviceException("Thiet bi " + device.MaThietBi + " da co trong phieu");
        }

        if (items.Count >= Borrower.GetMaximumDevices())
            throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());

        if (device.TrangThai != DeviceStatus.Available)
            throw new DeviceUnavailableException(device.MaThietBi, device.TrangThai);
    }

    // FIX quan trong: validate truoc, doi trang thai sau
    // Ly do: neu validate fail thi thiet bi van giu Available, phieu khong bi them rac
    public void AddDevice(TeachingDevice device, int borrowDays)
    {
        ValidateBorrowing(device, borrowDays);

        BorrowItem it = new BorrowItem(device, BorrowedDate, BorrowedDate.AddDays(borrowDays));
        device.MarkAsBorrowed(); // doi trang thai sau khi check xong
        items.Add(it);
    }

    public decimal ReturnDevice(string deviceId, DateTime returnedDate)
    {
        BorrowItem? found = null;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Device.MaThietBi == deviceId)
                found = items[i];
        }
        if (found == null)
            throw new DeviceNotInSlipException("Thiet bi " + deviceId + " khong thuoc phieu");

        // FIX: check tra 2 lan, nem ngoai le cho dung
        if (found.IsReturned)
            throw new DeviceAlreadyReturnedException("Thiet bi " + deviceId + " da tra truoc do");

        found.Return(returnedDate);
        found.Device.MarkAsAvailable();

        DateTime hanTra = found.BorrowedDate.AddDays(found.Device.MaxBorrowDays);
        int tre = (returnedDate - hanTra).Days;
        if (tre < 0) tre = 0;
        return found.Device.TinhTienTre(tre);
    }

    public decimal CalculateTotalLateFee()
    {
        decimal tong = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].IsReturned)
            {
                DateTime han = items[i].BorrowedDate.AddDays(items[i].Device.MaxBorrowDays);
                int tre = (((DateTime)items[i].ReturnedDate) - han).Days;
                if (tre > 0)
                    tong = tong + items[i].Device.TinhTienTre(tre);
            }
        }
        return tong;
    }
}
