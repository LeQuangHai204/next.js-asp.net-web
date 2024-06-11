namespace Api.Models
{
    public abstract class CreateDto<T> : IEntityDto<T>
        where T : IEntity
    {

    }
}