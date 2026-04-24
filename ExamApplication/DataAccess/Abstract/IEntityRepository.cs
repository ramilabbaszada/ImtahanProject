using ExamApplication.Entities.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;


namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task<T?> Get(Expression<Func<T, bool>> filter);
        Task Add(T entity);
        Task Update(T entity, Action<EntityEntry<T>>? rules = null);
        Task Delete(T entity);
    }
}

