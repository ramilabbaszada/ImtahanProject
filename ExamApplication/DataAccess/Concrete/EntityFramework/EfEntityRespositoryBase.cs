using Core.DataAccess;
using ExamApplication.DataAccess.Extensions;
using ExamApplication.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace ExamApplication.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
            where TEntity : class, IEntity, new()
    {
        protected DbContext context;
        public EfEntityRepositoryBase(DbContext context) 
        {
            this.context = context;
        }

        public async Task Add(TEntity entity)
        {
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await context.SaveChangesAsync();   
            context.Entry(entity).State = EntityState.Detached;
        }

        public async Task Delete(TEntity entity)
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
        }

        public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> filter)
        {
            return await context.Set<TEntity>().Where(filter).AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
                return filter == null ? await context.Set<TEntity>().AsNoTracking().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();

        }

        public async Task Update(TEntity entity, Action<EntityEntry<TEntity>>? rules)
        {
            var updatedEntity = context.Entry(entity);

            if (rules == null)
            {
                updatedEntity.State = EntityState.Modified;
                goto summary;
            }

            foreach (var property in typeof(TEntity).GetProperties().Where(propery => !propery.IsEditable()))
                updatedEntity.Property(property.Name).IsModified = false;

            rules(updatedEntity);

        summary:
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
        }
    }
}
