
using API.DAL.Helpers;
namespace API.DAL.Repositories.GenericRepo

{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<PaginationResult<T>> GetAllAsync(PagingParams PagingParams);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveChangesAsync();

    }
}
