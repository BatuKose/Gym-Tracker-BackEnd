using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IrepositoryBase<T>
    {
        
        IQueryable<T> FinAllByCondition(Expression<Func<T,bool>>expression,bool tracChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
