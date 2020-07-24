using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public interface IRepository <TSource>
    {
        TSource Create(TSource item);
        TSource Read(int id);
        bool Update(TSource item);
        bool Delete(int id);
        IQueryable<TSource> ReadAll();
        void CreateAll(IEnumerable<TSource> items);
    }
}
