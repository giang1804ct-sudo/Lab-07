/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/

namespace ThietBiDayHoc
{
    // Micro khong day
    public class WirelessMicrophone : TeachingDevice
    {
        public int BatteryLevel { get; private set; }
        public int SignalQuality { get; private set; } // 0 den 100

        public WirelessMicrophone(string id, string name, string location, int pin, int song)
            : base(id, name, location)
        {
            BatteryLevel = pin;
            SignalQuality = song;
        }

        public override bool IsReadyForUse()
        {
            // BUG: quen kiem tra SignalQuality, chi kiem tra pin va Status
            // TODO: thieu dieu kien tin hieu tu 40% tro len
            if (Status == DeviceStatus.Available && BatteryLevel >= 30)
                return true;
            return false;
        }

        public override double GetDefaultPriorityScore()
        {
            // diem = trung binh pin va chat luong tin hieu
            double diem = (BatteryLevel + SignalQuality) / 2.0;
            return diem;
        }
    }
}
