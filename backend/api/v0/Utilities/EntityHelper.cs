using System.Reflection;

namespace Api.Model
{
    public static class EntityHelper
    {
        public static void Merge<T>(this T origin, object data)
            where T : IDbEntity
        {
            (origin as object).Merge(data);
        }

        public static T ToEntity<T>(this IEntityDto<T> input)
            where T : class, IDbEntity
        {
            return Adapt<IEntityDto<T>, T>(input);
        }

        public static TDto ToDto<TEntity, TDto>(this TEntity input)
            where TEntity : class, IDbEntity
            where TDto : class, IEntityDto<TEntity>
        {
            return Adapt<TEntity, TDto>(input);
        }

        private static TOut Adapt<TIn, TOut>(TIn input)
            where TIn : class
            where TOut : class
        {
            TOut output = Activator.CreateInstance<TOut>();
            output.Merge(input);
            return output;
        }

        private static void Merge(this object origin, object data)
        {
            foreach (PropertyInfo p in data.GetType().GetProperties())
            {
                object? value = p.GetValue(data);
                PropertyInfo? property = origin.GetType().GetProperty(p.Name);
                if (value != null && property != null && property.CanWrite) property.SetValue(origin, value);
            }
        }
    }
}