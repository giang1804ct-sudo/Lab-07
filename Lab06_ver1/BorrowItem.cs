/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// 1 dong trong phieu muon

public class BorrowItem
{
    public TeachingDevice Device { get; set; }
    public DateTime BorrowedDate { get; set; }
    public int FromPeriod { get; set; }
    public int ToPeriod { get; set; }
    public DateTime? ReturnedDate { get; private set; }

    // Neu da co ngay tra thi coi nhu da tra
    public bool IsReturned
    {
        get { return ReturnedDate != null; }
    }

    public BorrowItem(TeachingDevice device, DateTime borrowedDate, DateTime dueDate)
    {
        Device = device;
        BorrowedDate = borrowedDate;
        // Tu tiet 1 den tiet 5 cho don gian, em lam cung
        FromPeriod = 1;
        ToPeriod = 5;
        // BUG: khong check device null, khong check dueDate < borrowedDate
        // TODO: can validate them
    }

    public void Return(DateTime returnedDate)
    {
        // BUG 1: khong kiem tra tra 2 lan, goi 2 lan van cho
        // BUG 2: khong kiem tra ngay tra truoc ngay muon
        ReturnedDate = returnedDate;
    }
}
