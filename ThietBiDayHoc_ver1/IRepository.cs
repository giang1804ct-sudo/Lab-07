/***************
MSSV: 2024xxxx
Ho ten: Nguyen Van A
***************/
using System;
using System.Collections.Generic;

namespace ThietBiDayHoc
{
    // Interface kho luu tru tong quat, dung generic
    public interface IRepository<T> where T : IIdentifiable
    {
        void Add(T item);
        bool Remove(string id);
        T FindById(string id);
        IReadOnlyList<T> GetAll();
        IReadOnlyList<T> FindAll(Predicate<T> condition);
    }
}
