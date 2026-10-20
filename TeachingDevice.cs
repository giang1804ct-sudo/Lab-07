/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/

namespace ThietBiDayHoc
{
    // Lop cha cho cac thiet bi day hoc
    public abstract class TeachingDevice : IIdentifiable
    {
        public string Id { get; }
        public string Name { get; }
        public string StorageLocation { get; }
        public DeviceStatus Status { get; protected set; }

        public TeachingDevice(string id, string name, string location)
        {
            // TODO: can xu ly them cho nay, chua kiem tra id rong hay null
            // em de tam vay cho chay duoc truoc da
            Id = id;
            Name = name;
            StorageLocation = location;
            Status = DeviceStatus.Available; // moi tao thi cho san sang luon
        }

        // 2 ham nay lop con tu cai dat lai (da hinh)
        public abstract bool IsReadyForUse();
        public abstract double GetDefaultPriorityScore();

        public void Borrow()
        {
            
            // Vi du pin 5% van cho muon duoc -> sai quy tac
            if (Status == DeviceStatus.Available)
            {
                Status = DeviceStatus.Borrowed;
            }
            else
            {
                throw new DeviceNotReadyException(Id, Status);
            }
        }

        public void Return()
        {
           
            
            if (Status == DeviceStatus.Borrowed)
            {
                Status = DeviceStatus.Available;
            }
        }
    }
}
