/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Kho luu trong bo nho (ban 3 em da sua)
    public class InMemoryRepository<T> : IRepository<T> where T : IIdentifiable
    {
        private List<T> ds = new List<T>(); // van dung ten ds nhu ban 1

        public void Add(T item)
        {
            // FIX: hom qua em quen check, gio em them vao
            if (item == null)
                throw new ArgumentNullException("item khong duoc null");
            if (FindById(item.Id) != null)
                throw new DuplicateIdException(item.Id);
            ds.Add(item);
        }

        public bool Remove(string id)
        {
            T timDuoc = FindById(id);
            if (timDuoc == null)
                return false;
            ds.Remove(timDuoc);
            return true;
        }

        public T FindById(string id)
        {
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].Id == id)
                    return ds[i];
            }
            return default(T);
        }

        public IReadOnlyList<T> GetAll()
        {
            // FIX: hom qua tra ve ds goc bi sai, nay em doi sang AsReadOnly()
            return ds.AsReadOnly();
        }

        public IReadOnlyList<T> FindAll(Predicate<T> condition)
        {
            List<T> kq = new List<T>();
            // van viet for nhu cu vi em chua quen Linq
            for (int i = 0; i < ds.Count; i++)
            {
                if (condition(ds[i]))
                    kq.Add(ds[i]);
            }
            return kq;
        }
    }
}
