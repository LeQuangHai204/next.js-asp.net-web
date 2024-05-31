using System.Reflection;

namespace Api
{
    public interface IEntityDtoMapper<TEntity>
        where TEntity : IDbEntity
    {
        TEntity ToEntity(IEntityDto<TEntity> dto);
    }
}
