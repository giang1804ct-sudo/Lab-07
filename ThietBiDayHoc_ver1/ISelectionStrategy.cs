/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
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
