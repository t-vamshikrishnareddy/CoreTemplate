using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoreTemplate.Repository.Associates
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();

        IEnumerable<T> GetAll();
        IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T GetById(long id);
        T GetById(string id);

        void Insert(T entity);
        bool Update(T entity);
        bool BulkUpdate(IEnumerable<T> entList);
        void Delete(T entity);
        void Attach(T entity);

    }
}
