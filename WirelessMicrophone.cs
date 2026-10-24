/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/

namespace ThietBiDayHoc
{
    // Micro khong day (ban da sua)
    public class WirelessMicrophone : TeachingDevice
    {
        public int BatteryLevel { get; private set; }
        public int SignalQuality { get; private set; }

        public WirelessMicrophone(string id, string name, string location, int pin, int song)
            : base(id, name, location)
        {
            BatteryLevel = pin;
            SignalQuality = song;
        }

        public override bool IsReadyForUse()
        {
            // FIX: hom qua em quen check tin hieu, nay em bo sung them
            if (Status == DeviceStatus.Available && BatteryLevel >= 30 && SignalQuality >= 40)
                return true;
            return false;
        }

        public override double GetDefaultPriorityScore()
        {
            double diem = (BatteryLevel + SignalQuality) / 2.0;
            return diem;
        }
    }
}
