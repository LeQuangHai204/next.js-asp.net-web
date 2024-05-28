namespace Api
{
    public interface IDbEntityDto<T> : IEntity
        where T : IDbEntity
    {

    }
}