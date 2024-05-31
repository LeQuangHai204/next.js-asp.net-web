
using System.Linq.Expressions;
using System.Reflection;

namespace Api.Models
{
    public class CustomerDtoMapper : IEntityDtoMapper<Customer>
    {
        public Customer ToEntity(IEntityDto<Customer> customerDto)
        {
            Dictionary<PropertyInfo, object?> properties = new();
            Type type = typeof(Customer);

            foreach (PropertyInfo property in type.GetProperties())
            {
                properties[property] = customerDto
                    .GetType()
                    .GetProperty(property.Name)?
                    .GetValue(customerDto);
            }

            return new Customer()
            {
                FullName = GetPropertyValue<string>(properties, type.GetProperty("FullName"))
                    ?? throw new ArgumentNullException("Convert failed: customer's full name is required"),
                NickName = GetPropertyValue<string>(properties, type.GetProperty("NickName")),
                Address = GetPropertyValue<string>(properties, type.GetProperty("Address")),
                City = GetPropertyValue<string>(properties, type.GetProperty("City")),
                PostalCode = GetPropertyValue<string>(properties, type.GetProperty("PostalCode")),
                Country = GetPropertyValue<string>(properties, type.GetProperty("Country"))
            };
        }

        private T? GetPropertyValue<T>(
            Dictionary<PropertyInfo, object?> properties,
            PropertyInfo? property)
            where T : class
        {
            if (property == null) throw new ArgumentNullException("Property not found");
            if (properties.TryGetValue(property, out object? value)) return (T?)value;
            return null;
        }

        public T ToDto<T>(Customer customer) where T : IEntityDto<Customer>
        {
            NewExpression newExpression = Expression.New(typeof(T));
            Expression<Func<T>> expression = Expression.Lambda<Func<T>>(newExpression);
            Func<T> compiledExpression = expression.Compile();
            // Use expression trees to create an instance of the DTO
            T dto = compiledExpression();

            // Set properties using reflection
            foreach (PropertyInfo customerProperty in typeof(Customer).GetProperties())
            {
                PropertyInfo? dtoProperty = typeof(T).GetProperty(customerProperty.Name);
                if (dtoProperty != null && dtoProperty.CanWrite)
                {
                    object? value = customerProperty.GetValue(customer);
                    dtoProperty.SetValue(dto, value);
                }
            }

            return dto;
        }
    }
}