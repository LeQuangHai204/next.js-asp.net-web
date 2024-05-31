using System.Reflection;

namespace Api
{
    public interface IEntityMapper<TEntity>
        where TEntity : IDbEntity
    {
        TEntity ToEntity(IEntityDto<TEntity> dto);
        IEntityDto<TEntity> ToDto<T>(TEntity entity)
            where T : IEntityDto<TEntity>, new();
    }
}
