/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang 
***************/
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Interface chien luoc chon thiet bi
    public interface ISelectionStrategy<T>
    {
        T Select(IReadOnlyList<T> candidates);
    }
}
