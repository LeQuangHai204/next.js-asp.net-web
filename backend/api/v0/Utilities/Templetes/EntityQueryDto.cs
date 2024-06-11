namespace Api.Models
{
    public abstract class EntityQueryDto<T> : IEntityDto<T>
        where T : IEntity
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}