/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Dieu khien dieu hoa
    public class AirConditionerRemote : TeachingDevice
    {
        public IReadOnlySet<string> CompatibleRooms { get; }

        public AirConditionerRemote(string id, string name, string location, List<string> rooms)
            : base(id, name, location)
        {
            CompatibleRooms = new HashSet<string>(rooms);
        }

        public bool IsCompatibleWith(string roomId)
        {
            
            return CompatibleRooms.Contains(roomId);
        }

        public override bool IsReadyForUse()
        {
            // chi can Available la san sang
            if (Status == DeviceStatus.Available)
                return true;
            return false;
        }

        public override double GetDefaultPriorityScore()
        {
            
            return 100;
        }
    }
}
