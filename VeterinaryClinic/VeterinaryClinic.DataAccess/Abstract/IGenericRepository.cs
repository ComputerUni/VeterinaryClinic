using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.DataAccess.Abstract
{
    public interface IGenericRepository<T>
    {
        List<T> List();
        void Insert(T p);
        T Get(Expression<Func<T, bool>> filter);
        void Update(T p);
        void Delete(T p);
        List<T> List(Expression<Func<T, bool>> filter);


        //Task<List<T>> ListAsync();
        //Task InsertAsync(T p);
        //Task<T> GetAsync(Expression<Func<T, bool>> filter);
        //Task UpdateAsync(T p);
        //Task DeleteAsync(T p);
        //Task<List<T>> ListAsync(Expression<Func<T, bool>> filter);


    }
}
