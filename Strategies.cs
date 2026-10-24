/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang 
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Chon cai co diem cao nhat
    public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
    {
        private readonly Func<T, double> scoreSelector; // ham tinh diem

        public HighestPriorityStrategy(Func<T, double> scoreSelector)
        {
            this.scoreSelector = scoreSelector;
        }

        public T Select(IReadOnlyList<T> candidates)
        {
          
            T totNhat = candidates[0]; // em dat ten best -> totNhat cho de hieu
            double diemCaoNhat = scoreSelector(totNhat);
            // em viet for tu 1 vi phan tu 0 lay roi
            for (int i = 1; i < candidates.Count; i++)
            {
                double diem = scoreSelector(candidates[i]);
                if (diem > diemCaoNhat)
                {
                    diemCaoNhat = diem;
                    totNhat = candidates[i];
                }
            }
            return totNhat;
        }
    }

    // Chon cai dau tien trong danh sach
    public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
    {
        public T Select(IReadOnlyList<T> candidates)
        {
           
            return candidates[0];
        }
    }
}
