/***************

MSSV: 202418889
Họ tên: Đặng Thị Hương Giang

***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Kho luu trong bo nho, dung List cho de hieu
   public class InMemoryRepository<T> : IRepository<T> where T : class, IIdentifiable

    {
        private List<T> ds = new List<T>(); // em dat ten ds cho ngan gon


        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("Item khong duoc de trongo");

            if (ds.Any(x => x.Id == item.Id))
                throw new DuplicateIdException(item.Id); // Dùng exception da co trong Exceptions.cs

            ds.Add(item); // Them chi khi hop le
        }

        public T FindById(string id)
        {
            // tim theo ma, em viet vong for cho de nhin
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].Id == id)
                    return ds[i];
            }
            return null; 
        }

        public bool Remove(string id)
        {
            T timDuoc = FindById(id); // em doi ten found -> timDuoc cho de hieu
            if (timDuoc == null)
                return false;
            ds.Remove(timDuoc);
            return true;
        }

        public IReadOnlyList<T> GetAll()
        {
            
            return ds;
        }

        public IReadOnlyList<T> FindAll(Predicate<T> condition)
        {
            List<T> kq = new List<T>();
            // em viet for thuong
            for (int i = 0; i < ds.Count; i++)
            {
                if (condition(ds[i]))
                    kq.Add(ds[i]);
            }
            return kq;
        }
    }
}
