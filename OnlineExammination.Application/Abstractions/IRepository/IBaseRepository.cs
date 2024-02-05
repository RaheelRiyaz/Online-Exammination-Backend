using System.Linq.Expressions;

namespace OnlineExammination.Application.Abstractions.IRepository
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(Guid id);
        Task<int> Delete(T entity);
        Task<int> DeleteRange(List<Guid> ids);

        Task<int> DeleteRange(List<T> entities);
        Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> expression);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<bool> IsExists(Expression<Func<T, bool>> expression);
        Task<int> InsertRange(IEnumerable<T> entities);


        #region 
        /*   Task<IEnumerable<T1>> QueryAsync<T1>(string sql);
           Task<int> ExecuteAsync(string sql);*/
        #endregion

    }
}
