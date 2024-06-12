namespace Api
{
    public interface IEntityRepository<T>
        where T : IDbEntity
    {
        IAsyncEnumerable<BasicInfoDto<T>> GetAllAsync(EntityQueryDto<T> entity);
        Task<BasicInfoDto<T>?> GetByIdAsync(object id);
        Task<BasicInfoDto<T>> AddAsync(CreateDto<T> entity);
        Task<BasicInfoDto<T>?> UpdateAsync(UpdateDto<T> entity, object id);
        Task DeleteAsync(object id);
    }
}