/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Dieu khien dieu hoa (ban da sua)
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
            // FIX: hom qua em go "a203" bi sai, nay em cho so sanh thuong/hoa giong nhau
            if (string.IsNullOrWhiteSpace(roomId))
                return false;
            foreach (var r in CompatibleRooms) // em viet foreach cho gon
            {
                if (r.ToLower() == roomId.ToLower())
                    return true;
            }
            return false;
        }

        public override bool IsReadyForUse()
        {
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
