using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Data.Spec
{
      public interface IRepository<T> where T : class
      {
             
          IQueryable<T> GetChildren(params Expression<Func<T, object>>[] expression);

           IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression);

           IQueryable<T> GetAll();
           T Update(T entity);
           T Insert(T entity);
           T GetById(T entity);
           void Delete(T entity);
           void Save();
          void Dispose();
      }

    }

