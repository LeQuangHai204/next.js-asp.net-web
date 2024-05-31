namespace Api
{
    public interface IEntityDao<T>
        where T : IDbEntity
    {
        IEnumerable<IEntityDto<T>> GetAllAsync();
        Task<IEntityDto<T>?> GetByIdAsync(int id);
        Task<T> AddAsync(IEntityDto<T> entity);
        Task UpdateAsync(IEntityDto<T> entity, object id);
        Task DeleteAsync(int id);
    }
}
