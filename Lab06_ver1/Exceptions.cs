/****************
Ma sinh vien: 22ITxxx
Ho ten: Nguyen Van A
****************/
// File ngoai le chung

public class BorrowingException : Exception
{
    public BorrowingException(string message) : base(message)
    {
    }

    public BorrowingException(string message, Exception inner) : base(message, inner)
    {
    }
}

// Thiet bi khong san sang thi khong cho muon
public class DeviceUnavailableException : BorrowingException
{
    public string DeviceId { get; }
    public DeviceStatus CurrentStatus { get; }

    public DeviceUnavailableException(string id, DeviceStatus st)
        : base("Thiet bi " + id + " dang o trang thai " + st + " nen khong muon duoc")
    {
        DeviceId = id;
        CurrentStatus = st;
    }
}

// Vuot qua so luong duoc muon
public class BorrowLimitExceededException : BorrowingException
{
    public string BorrowerId { get; }
    public int MaximumDevices { get; }

    public BorrowLimitExceededException(string borrowerId, int max)
        : base("Nguoi " + borrowerId + " chi duoc muon toi da " + max + " thiet bi")
    {
        BorrowerId = borrowerId;
        MaximumDevices = max;
    }
}

// So ngay muon khong hop le
public class InvalidBorrowPeriodException : BorrowingException
{
    public string DeviceId { get; }
    public int RequestedDays { get; }
    public int MaxDays { get; }

    public InvalidBorrowPeriodException(string deviceId, int req, int max)
        : base("So ngay muon " + req + " khong hop le, toi da " + max)
    {
        DeviceId = deviceId;
        RequestedDays = req;
        MaxDays = max;
    }
}

public class DuplicateDeviceException : BorrowingException
{
    public DuplicateDeviceException(string msg) : base(msg) { }
}

public class DeviceNotInSlipException : BorrowingException
{
    public DeviceNotInSlipException(string msg) : base(msg) { }
}

public class DeviceAlreadyReturnedException : BorrowingException
{
    public DeviceAlreadyReturnedException(string msg) : base(msg) { }
}
