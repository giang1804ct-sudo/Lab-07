/***************
MSSV: 202418889
Họ tên: Đặng Thị Hương Giang
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Kho luu trong bo nho, dung List cho de hieu
    public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
    {
        private List<T> ds = new List<T>(); // em dat ten ds cho ngan gon

        public void Add(T item)
        {
            // BUG 1: quen kiem tra null, quen kiem tra trung ma
            // TODO: can xu ly them cho nay, phai throw DuplicateIdException neu trung
            ds.Add(item);
        }

        public bool Remove(string id)
        {
            T timDuoc = FindById(id); // em doi ten found -> timDuoc cho de hieu
            if (timDuoc == null)
                return false;
            ds.Remove(timDuoc);
            return true;
        }

        public T FindById(string id)
        {
            // tim theo ma, em viet vong for cho de nhin
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].Id == id)
                    return ds[i];
            }
            return default(T); // khong thay thi tra ve null
        }

        public IReadOnlyList<T> GetAll()
        {
            // BUG 2: tra ve list goc, nguoi ngoai co the sua truc tiep
            // TODO: nen tra ve ban copy .AsReadOnly()
            return ds;
        }

        public IReadOnlyList<T> FindAll(Predicate<T> condition)
        {
            List<T> kq = new List<T>();
            // em viet for thuong, khong dung Linq vi chua quen
            for (int i = 0; i < ds.Count; i++)
            {
                if (condition(ds[i]))
                    kq.Add(ds[i]);
            }
            return kq;
        }
    }
}
