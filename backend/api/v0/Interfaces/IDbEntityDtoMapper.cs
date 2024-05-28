namespace Api
{
    public interface IDbEntityDtoMapper<T>
        where T : IDbEntity
    {
        T ConvertCreateRequestDtoToEntity(IDbEntityDto<T> dto);
    }
}
