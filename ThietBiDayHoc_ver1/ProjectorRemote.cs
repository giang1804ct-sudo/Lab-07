/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
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
            // BUG: quen kiem tra Status, chi kiem tra pin
            // TODO: can xu ly them cho nay, phai them Status == Available nua, de mai sua
            if (BatteryLevel >= 20) // em nho thay bao pin tu 20% tro len
                return true;
            return false;
        }

        public override double GetDefaultPriorityScore()
        {
            // diem = muc pin, don gian
            return BatteryLevel;
        }
    }
}
