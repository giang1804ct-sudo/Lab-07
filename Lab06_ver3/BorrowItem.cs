/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
Lab 06 ver3 - da sua loi
****************/

public class BorrowItem
{
    public TeachingDevice Device { get; set; }
    public DateTime BorrowedDate { get; set; }
    public int FromPeriod { get; set; }
    public int ToPeriod { get; set; }
    public DateTime? ReturnedDate { get; private set; }

    public bool IsReturned
    {
        get { return ReturnedDate != null; }
    }

    public BorrowItem(TeachingDevice device, DateTime borrowedDate, DateTime dueDate)
    {
        // FIX 1: check null, tranh loi sau nay
        if (device == null)
            throw new ArgumentException("Device khong duoc null");

        // FIX 2: han tra phai sau ngay muon, khong thi bao loi
        if (dueDate < borrowedDate)
            throw new InvalidBorrowPeriodException(device.MaThietBi, 0, device.MaxBorrowDays);

        Device = device;
        BorrowedDate = borrowedDate;
        FromPeriod = 1;
        ToPeriod = 5;
    }

    public void Return(DateTime returnedDate)
    {
        // FIX 3: khong cho tra 2 lan
        if (IsReturned)
            throw new DeviceAlreadyReturnedException("Thiet bi " + Device.MaThietBi + " da tra roi");

        // FIX 4: ngay tra khong duoc truoc ngay muon
        if (returnedDate < BorrowedDate)
            throw new ArgumentException("Ngay tra khong duoc truoc ngay muon");

        ReturnedDate = returnedDate;
    }
}
