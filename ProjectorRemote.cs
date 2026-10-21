/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/
namespace ThietBiDayHoc
{
    // Dieu khien may chieu
    public class ProjectorRemote : TeachingDevice
    {
        public int BatteryLevel { get; private set; } // pin tu 0 den 100

        public ProjectorRemote(string id, string name, string location, int pin)
            : base(id, name, location)
        {
            BatteryLevel = pin;
        }

        public override bool IsReadyForUse()
        {
            
            if (BatteryLevel >= 20) 
                return true;
            return false;
        }

        public override double GetDefaultPriorityScore()
        {
            // diem = muc pin
            return BatteryLevel;
        }
    }
}
