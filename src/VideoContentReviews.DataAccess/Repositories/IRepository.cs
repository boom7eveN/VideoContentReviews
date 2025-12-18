using System.Linq.Expressions;
using VideoContentReviews.DataAccess.Entities;

namespace VideoContentReviews.DataAccess.Repositories;

public interface IRepository<T> where T : class, IBaseEntity
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    
    T? GetById(int id);
    Task<T?> GetByIdAsync(int id);
    T? GetById(Guid id);
    Task<T?> GetByIdAsync(Guid id);

 

    T Save(T entity);
    Task<T> SaveAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(T entity);
}