/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/

namespace ThietBiDayHoc
{
    // Dieu khien may chieu (ban da sua)
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; private set; }

        public ProjectorRemote(string id, string name, string location, int pin)
            : base(id, name, location)
        {
            BatteryLevel = pin;
        }

        public override bool IsReadyForUse()
        {
            // FIX: hom qua em quen check Status, nay em them vao
            if (Status == DeviceStatus.Available && BatteryLevel >= 20)
                return true;
            return false;
        }

        public override double GetDefaultPriorityScore()
        {
            return BatteryLevel; // diem = pin
        }
    }
}
