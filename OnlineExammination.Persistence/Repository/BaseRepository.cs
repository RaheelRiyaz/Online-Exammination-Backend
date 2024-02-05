using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineExammination.Application.Abstractions.IRepository;
using OnlineExammination.Domain.Entities;
using OnlineExammination.Persistence.Data;
using System.Linq.Expressions;

namespace OnlineExammination.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T:BaseEntity,new()
    {
        protected readonly OnlineExamminationDbContext context;

        public BaseRepository(OnlineExamminationDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Add(T entity)
        {
            context.Set<T>().Add(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid id)
        {
            context.Set<T>().Remove(new T() { Id=id});

            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T entity)
        {
            context.Set<T>().Remove(entity);

            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteRange(List<Guid> ids)
        {
            List<T> entities = new List<T>();

            foreach (var id in ids)
            {
                entities.Add(new T() { Id = id });
            }
            context.Set<T>().RemoveRange(entities);

            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteRange(List<T> entities)
        {
            context.Set<T>().RemoveRange(entities);

            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> expression)
        {

            return await context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
           return await context.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExists(Expression<Func<T, bool>> expression)
        {
          
            return await context.Set<T>().AnyAsync(expression);
        }

     
        public async Task<int> Update(T entity)
        {
            context.Set<T>().Update(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> InsertRange(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);

            return await context.SaveChangesAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        #region
        /* public async Task<IEnumerable<T1>> QueryAsync<T1>(string sql)
         {
             try
             {
                 SqlConnection con = new(context.Database.GetConnectionString());
                 return await con.QueryAsync<T1>(sql);
             }
             catch(Exception ex)
             {
                 throw new Exception(ex.Message);
             }
         }

         public async Task<int> ExecuteAsync(string sql)
         {
             try
             {
                 SqlConnection con = new(context.Database.GetConnectionString());
                 return await con.ExecuteAsync(sql);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }
         }*/
        #endregion
    }

    public static class DapperExtensionMethods
    {
        public async static Task<int> ExecuteAsync(this DbContext context, string sql,Object? param=null)
        {
            try
            {
                SqlConnection con = new(context.Database.GetConnectionString());
                return await con.ExecuteAsync(sql,param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async static Task<IEnumerable<T1>> QueryAsync<T1>(this DbContext context, string sql,Object? param=default)
        {
            try
            {
                SqlConnection con = new(context.Database.GetConnectionString());
                return await con.QueryAsync<T1>(sql,param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async static Task<T1?> FirstOrDefaulyAsync<T1>(this DbContext context, string sql,object? param=null)
        {
            try
            {
                SqlConnection con = new(context.Database.GetConnectionString());
                return await con.QueryFirstOrDefaultAsync<T1>(sql,param);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
