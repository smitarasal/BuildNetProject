using QSI.Data.Spec;
using QSI.Domain;
using QSI.Domain.Spec;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QSI.Data
{
   public  class Repository<T> :  IRepository<T> where T : class
    {


       private readonly QSIEntities _context;

       public Repository()
       {
           if(_context==null)
           _context = new QSIEntities();
       }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns></returns>
        public  IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> GetChildren(params Expression<Func<T, object>>[] expression) 
        {
            IQueryable<T> retValue = _context.Set<T>();
            foreach (var item in expression)
            {
                retValue=  retValue.Include(item);
            }
            return retValue;
        }
        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            IEnumerable<T> query = _context.Set<T>().Where(expression).AsEnumerable();
            return query;
           
        
        }
        public T Update(T entity)
        {
            var result = _context.Set<T>().Attach(entity);
            _context.Entry(entity).State =System.Data.Entity.EntityState.Modified;
            return result;
        }

        public T Insert(T entity)
        {
            return _context.Set<T>().Add(entity);
        }
        public T GetById(T entity)
        {
            return _context.Set<T>().Find(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
       
    }
}
