using Api.Models;

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
