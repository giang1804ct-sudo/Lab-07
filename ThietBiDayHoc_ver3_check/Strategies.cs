/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Chon cai co diem cao nhat (ban 3 da sua)
    public class HighestPriorityStrategy<T> : ISelectionStrategy<T>
    {
        private readonly Func<T, double> scoreSelector;

        public HighestPriorityStrategy(Func<T, double> scoreSelector)
        {
            this.scoreSelector = scoreSelector;
        }

        public T Select(IReadOnlyList<T> candidates)
        {
            // FIX: hom qua em bi crash khi list rong, nay em them check
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException("Khong co ung vien nao de chon.");
            T totNhat = candidates[0]; // van de ten totNhat nhu ban 1
            double diemCaoNhat = scoreSelector(totNhat);
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

    // Chon cai dau tien (ban 3 da sua)
    public class FirstAvailableStrategy<T> : ISelectionStrategy<T>
    {
        public T Select(IReadOnlyList<T> candidates)
        {
            // FIX: them kiem tra rong giong o tren
            if (candidates == null || candidates.Count == 0)
                throw new NoSuitableItemException("Khong co ung vien nao de chon.");
            return candidates[0];
        }
    }
}
