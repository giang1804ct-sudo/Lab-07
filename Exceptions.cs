/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang
***************/

namespace ThietBiDayHoc
{
    // Cac loi dung trong bai
    public class DuplicateIdException : Exception
    {
        public string DuplicateId { get; }
        public DuplicateIdException(string id) : base("Trung ma: " + id)
        {
            DuplicateId = id;
        }
    }

    public class NoSuitableItemException : Exception
    {
        public NoSuitableItemException(string msg) : base(msg) { }
    }

    public class DeviceNotReadyException : Exception
    {
        public string DeviceId { get; }
        public DeviceStatus CurrentStatus { get; }
        public DeviceNotReadyException(string id, DeviceStatus status)
            : base("Thiet bi " + id + " chua san sang.")
        {
            DeviceId = id;
            CurrentStatus = status;
        }
    }
}
