namespace Api
{
    public interface IEntityDao<T>
        where T : IDbEntity
    {
        IAsyncEnumerable<T> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        Task<T?> UpdateAsync(T entity, object modifiedData);
        Task DeleteAsync(T entity);
    }
}

/*
namespace Api
{
    public interface IEntityDao<T>
        where T : IDbEntity
    {
        IAsyncEnumerable<BasicInfoDto<T>> GetAllAsync(EntityQueryDto<T> entity);
        Task<BasicInfoDto<T>?> GetByIdAsync(int id);
        Task<T> AddAsync(CreateDto<T> entity);
        Task<BasicInfoDto<T>?> UpdateAsync(UpdateDto<T> entity, object id);
        Task DeleteAsync(int id);
    }
}

*/