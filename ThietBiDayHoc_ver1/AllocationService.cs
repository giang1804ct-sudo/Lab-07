/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Dich vu cap phat tong quat, dung duoc cho nhieu loai thiet bi
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
            strategy = newStrategy; // doi chien luoc luc chay, khong can sua code
        }

        public T Allocate(Predicate<T> additionalCondition)
        {
            // B1: lay tat ca tu kho
            IReadOnlyList<T> all = repository.GetAll();

            // B2: loc cai san sang + dieu kien phu, em viet for cho de hieu, chua quen Linq
            List<T> dsChon = new List<T>(); // em doi ten ungVien -> dsChon
            for (int i = 0; i < all.Count; i++)
            {
                // BUG: quen goi IsReadyForUse(), chi loc theo additionalCondition
                // TODO: can them all[i].IsReadyForUse() nua, de mai em sua
                if (additionalCondition(all[i]))
                {
                    dsChon.Add(all[i]);
                }
            }

            // B3: dung chien luoc de chon
            T chon = strategy.Select(dsChon);

            // B4: goi Borrow roi tra ve
            chon.Borrow();
            return chon;
        }
    }
}
