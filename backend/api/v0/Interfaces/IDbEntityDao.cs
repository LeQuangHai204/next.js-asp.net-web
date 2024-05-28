namespace Api
{
    public interface IDbEntityDao<T>
        where T : IDbEntity
    {
        IAsyncEnumerable<IDbEntityDto<T>> GetAllAsync();
        Task<IDbEntityDto<T>?> GetByIdAsync(int id);
        Task<T> AddAsync(IDbEntityDto<T> entity);
        Task UpdateAsync(IDbEntityDto<T> entity, object id);
        Task DeleteAsync(int id);
    }
}
