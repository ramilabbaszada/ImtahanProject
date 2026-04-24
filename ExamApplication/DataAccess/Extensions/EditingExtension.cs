using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace ExamApplication.DataAccess.Extensions
{
    public static class EditingExtension
    {
        public static bool IsEditable(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null || !propertyInfo.CanWrite || propertyInfo.PropertyType.IsClass || typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                return false;
            return true;
        }

        public static EntityEntry<TEntity> SetValue<TEntity, TProperty>(this EntityEntry<TEntity> entityEntry, Expression<Func<TEntity, TProperty>> expression, TProperty property) where TEntity : class
        {

            entityEntry.Property(expression).IsModified = true;
            entityEntry.Property(expression).CurrentValue = property;
            return entityEntry;
        }

    }
}
