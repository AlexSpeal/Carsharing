using System.Linq.Expressions;
using DataAccess.Entities;

namespace DataAccess.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    T? GetById(long id);
    T? GetById(Guid id);
    T Save(T entity);
    void Delete(T entity);
}