/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Dich vu cap phat tong quat (ban da sua)
    public class AllocationService<T> where T : TeachingDevice
    {
        private readonly IRepository<T> repository;
        private ISelectionStrategy<T> strategy;

        public AllocationService(IRepository<T> repository, ISelectionStrategy<T> strategy)
        {
            this.repository = repository;
            this.strategy = strategy;
        }

        public void ChangeStrategy(ISelectionStrategy<T> newStrategy)
        {
            strategy = newStrategy;
        }

        public T Allocate(Predicate<T> additionalCondition)
        {
            IReadOnlyList<T> all = repository.GetAll();

            List<T> dsChon = new List<T>(); // van de ten dsChon giong ban 1 cho de theo doi
            for (int i = 0; i < all.Count; i++)
            {
                // FIX: hom qua em quen goi IsReadyForUse() nen may pin yeu van bi chon
                if (all[i].IsReadyForUse() && additionalCondition(all[i]))
                {
                    dsChon.Add(all[i]);
                }
            }

            // Neu khong co ai thi Select se nem loi, em thay hop ly nen de vay
            T chon = strategy.Select(dsChon);
            chon.Borrow(); // goi Borrow de chuyen sang Borrowed
            return chon;
        }
    }
}
