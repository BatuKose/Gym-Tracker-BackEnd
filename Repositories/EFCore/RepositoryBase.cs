using Entites.Exceptions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryBase<T> : IrepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
            }
            catch (Exception)
            {

                throw new DBExceptions("Veritabanı hatası logları inceleyin.");
            }
        }


        public void Delete(T entity)
        {
            try
            {
               
                _context.Set<T>().Remove(entity);
            }

            catch (Exception)
            {
                     
                throw new DBExceptions("Veritabanı hatası logları inceleyin.");
            }

        }



        public IQueryable<T> FinAllByCondition(Expression<Func<T, bool>> expression, bool tracChanges)
        {
            try
            {
                return
                !tracChanges ?
                 _context.Set<T>().Where(expression).AsNoTracking() :
                  _context.Set<T>().Where(expression);
            }
            catch (Exception)
            {

                throw new DBExceptions("Veritabanı hatası logları inceleyin.");
            }


        }


        public void Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
            }
            catch (Exception)
            {

                throw new DBExceptions("Veritabanı hatası logları inceleyin.");
            }

        }
       
    }
}
