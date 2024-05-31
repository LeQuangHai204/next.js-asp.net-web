
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

            Func<PropertyInfo?, object?> getPropertyValue = (prop) =>
            {
                if (prop == null) throw new ArgumentNullException("Property not found");
                return properties.TryGetValue(prop, out object? value) ? value : null;
            };

            return new Customer()
            {
                FullName = getPropertyValue(type.GetProperty("FullName")) as string
                    ?? throw new ArgumentNullException("Convert failed: customer's full name is required"),
                NickName = getPropertyValue(type.GetProperty("NickName")) as string,
                Address = getPropertyValue(type.GetProperty("Address")) as string,
                City = getPropertyValue(type.GetProperty("City")) as string,
                PostalCode = getPropertyValue(type.GetProperty("PostalCode")) as string,
                Country = getPropertyValue(type.GetProperty("Country")) as string
            };
        }

        public T ToDto<T>(Customer customer) where T : IEntityDto<Customer>
        {
            NewExpression newExpression = Expression.New(typeof(T));
            Expression<Func<T>> expression = Expression.Lambda<Func<T>>(newExpression);
            Func<T> compiledExpression = expression.Compile();
            // Use expression trees to create an instance of the DTO
            T dto = compiledExpression();

            // Set properties using reflection
            foreach (PropertyInfo property in typeof(Customer).GetProperties())
            {
                PropertyInfo? dtoProperty = typeof(T).GetProperty(property.Name);
                if (dtoProperty != null && dtoProperty.CanWrite)
                {
                    object? value = property.GetValue(customer);
                    dtoProperty.SetValue(dto, value);
                }
            }

            return dto;
        }
    }
}