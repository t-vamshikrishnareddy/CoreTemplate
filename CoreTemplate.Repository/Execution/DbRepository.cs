using CoreTemplate.Enum;
using CoreTemplate.Repository.Associates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoreTemplate.Repository.Execution
{

    public class DbRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        protected DbContext context;

        public DbRepository(DbContext dataContext)
        {
            this.context = dataContext;
            //DbSet = dataContext.Set<T>();
        }
        public DbRepository(DbSet<T> dbSet)
        {
            this.DbSet = dbSet;
        }

        #region IRepository<T> Members

        public void Insert(T entity)
        {
            DbSet.Add(entity);
            this.context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            DbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Deleted;
        }

        public IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }
        public T GetById(long id)
        {
            lock (context)
            {
                return DbSet.Find(id);
            }
        }
        public T GetById(string id)
        {
            return DbSet.Find(id);
        }

        public bool Update(T entity)
        {
            var entry = this.context.Entry(entity);
            var key = this.GetPrimaryKey(entry);

            if (entry.State == EntityState.Detached)
            {
                var currentEntry = this.DbSet.Find(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.context.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    this.DbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                }
                return true;
            }
            else
            {
                this.DbSet.Attach(entity);
                entry.State = EntityState.Modified;
            }
            return false;
        }

        public bool BulkUpdate(IEnumerable<T> entList)
        {
            Type entityType = typeof(T);
            var prop = entityType.GetProperty("ObjectState");

            foreach (T course in entList)
            {
                object obj = prop.GetValue(course, null);
                RowState rstate = obj == null ? RowState.Unchanged : (RowState)obj;

                switch (rstate)
                {
                    case RowState.Added:
                        this.Insert(course);
                        break;
                    case RowState.Modified:
                        this.Update(course);
                        break;
                    case RowState.Deleted:
                        this.Delete(course);
                        break;

                };
            }
            return true;

        }

        public bool BulkUpdateList(List<T> entList)
        {
            Type entityType = typeof(T);
            var prop = entityType.GetProperty("ObjectState");

            foreach (T course in entList)
            {
                object obj = prop.GetValue(course, null);
                RowState rstate = obj == null ? RowState.Unchanged : (RowState)obj;

                switch (rstate)
                {
                    case RowState.Added:
                        this.Insert(course);
                        break;
                    case RowState.Modified:
                        this.Update(course);
                        break;
                    case RowState.Deleted:
                        this.Delete(course);
                        break;

                };
            }
            return true;

        }

        public IQueryable<T> AsQueryable()
        {
            throw new NotImplementedException();
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Attach(T entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private
        private object GetPrimaryKey(EntityEntry entry)
        {
            var myObject = entry.Entity;
            var property = myObject.GetType().GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));
            return (object)property.GetValue(myObject, null);
        }
        #endregion



    }
}
