/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/

namespace ThietBiDayHoc
{
    // Lop cha cho cac thiet bi day hoc (ban da sua loi)
    public abstract class TeachingDevice : IIdentifiable
    {
        public string Id { get; }
        public string Name { get; }
        public string StorageLocation { get; }
        public DeviceStatus Status { get; protected set; }

        public TeachingDevice(string id, string name, string location)
        {
            // FIX: them kiem tra dau vao, truoc do ban 1 quen nen id rong van chay
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id khong duoc rong");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name khong duoc rong");
            Id = id;
            Name = name;
            StorageLocation = location;
            Status = DeviceStatus.Available;
        }

        public abstract bool IsReadyForUse();
        public abstract double GetDefaultPriorityScore();

        public void Borrow()
        {
            // FIX: them IsReadyForUse() vao moi dung
            // neu pin yeu thi khong cho muon nua
            if (Status == DeviceStatus.Available && IsReadyForUse())
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
            // FIX: truoc em de im lang, gio em cho bao loi luon cho de biet
            if (Status == DeviceStatus.Borrowed)
            {
                Status = DeviceStatus.Available;
            }
            else
            {
                throw new InvalidOperationException("Thiet bi " + Id + " chua duoc muon nen khong tra duoc.");
            }
        }
    }
}
