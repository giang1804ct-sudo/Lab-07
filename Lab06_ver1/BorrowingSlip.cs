/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// Phieu muon, 1 nguoi muon nhieu thiet bi

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

    // Them thiet bi vao phieu - BAN LAM SAI, THAY BAO KHONG DC LAM THE NAY
    public void AddDevice(TeachingDevice device, int borrowDays)
    {
        // BUG nghiem trong: doi trang thai truoc khi kiem tra
        // Neu throw thi thiet bi van bi Borrowed, mat nhat quan
        device.MarkAsBorrowed();

        if (items.Count >= Borrower.GetMaximumDevices())
        {
            throw new BorrowLimitExceededException(Borrower.BorrowerId, Borrower.GetMaximumDevices());
        }

        // BUG: chua check null, chua check trung, chua check Available, chua check borrowDays
        // TODO: can viet ham Validate rieng

        BorrowItem it = new BorrowItem(device, BorrowedDate, BorrowedDate.AddDays(borrowDays));
        items.Add(it);
    }

    // Tra thiet bi
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

        // BUG: quen check da tra roi, tra 2 lan van tinh tien
        // TODO: phai nem DeviceAlreadyReturnedException

        found.Return(returnedDate);
        found.Device.MarkAsAvailable();

        // tinh tien tre don gian
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
