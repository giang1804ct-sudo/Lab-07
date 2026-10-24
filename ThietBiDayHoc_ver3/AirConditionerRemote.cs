/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Dieu khien dieu hoa (ban 3 da sua)
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
            // Giu don gian nhu cu, nhung ghi ro: dieu hoa khong co diem mac dinh,
            // khi chon se truyen them dieu kien phong o ngoai (Allocate)
            return 100;
        }
    }
}
