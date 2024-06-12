namespace Api
{
    public interface IEntityDto<T> : IEntity
        where T : IEntity
    {

    }
}