using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq.Expressions;

namespace XML.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Detach(TEntity entity);

    }
}
