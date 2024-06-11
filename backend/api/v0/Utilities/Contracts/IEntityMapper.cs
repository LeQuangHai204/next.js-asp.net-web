namespace Api
{
    public interface IEntityMapper<T>
        where T : IEntity
    {
        T ToEntity(IEntityDto<T> dto);
        IEntityDto<T> ToDto<TDto>(T entity) where TDto : IEntityDto<T>, new();
        void Merge(T entity, IEntityDto<T> dto);
    }
}
