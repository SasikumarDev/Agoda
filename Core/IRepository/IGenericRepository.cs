using System.Linq.Expressions;
namespace Agoda.Core.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task Add(T data);
    Task<IEnumerable<T>> getAll();
    Task<IEnumerable<T>> getByCondition(Expression<Func<T, bool>> predicate);
    Task<T> getOneByCondition(Expression<Func<T, bool>> predicate);
}