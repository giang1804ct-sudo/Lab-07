/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
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
            // BUG: khong xu ly roomId null hoac viet hoa/thuong
            // Vi du "a203" vs "A203" se tra ve false sai
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
            // BUG: tra ve 100 cung, chua tinh theo phong
            // TODO: can xu ly them cho nay, tuy phong moi co diem khac nhau
            return 100;
        }
    }
}
