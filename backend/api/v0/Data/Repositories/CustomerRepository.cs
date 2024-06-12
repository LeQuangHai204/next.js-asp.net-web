using System.Linq.Expressions;
using System.Reflection;

using Api.Model.Dtos;

namespace Api.Model.Repositories
{
    public class CustomerRepository : IEntityRepository<Customer>
    {
        private readonly IEntityDao<Customer> _customerDao;
        private static readonly Dictionary<HttpMethod, Type> _inputTypes = new()
        {
            {HttpMethod.Get, typeof(CustomerQueryDto)},
            {HttpMethod.Post, typeof(CustomerCreateDto)},
            {HttpMethod.Put, typeof(CustomerUpdateDto)}
        };

        public CustomerRepository(IEntityDao<Customer> customerDao)
        {
            _customerDao = customerDao;
        }

        public IAsyncEnumerable<BasicInfoDto<Customer>> GetAllAsync(EntityQueryDto<Customer> query)
        {
            if (query.GetType() != _inputTypes[HttpMethod.Get]) throw new ArgumentException("Invalid type");

            IEnumerable<PropertyInfo> properties = query
                .GetType()
                .GetProperties()
                .Where(property => property.GetValue(query) != null
                    && property.Name != "PageIndex"
                    && property.Name != "PageSize");

            Expression<Func<Customer, bool>> initialExpression = Expression.Lambda<Func<Customer, bool>>(
                Expression.Constant(true),
                Expression.Parameter(typeof(Customer), "customer"));

            Expression<Func<Customer, bool>> filterExpression = properties.Aggregate(initialExpression, (current, property) =>
            {
                object? queryValue = property.GetValue(query);
                ParameterExpression parameterExpression = Expression.Parameter(typeof(Customer), "customer");
                MemberExpression propertyExpression = Expression.Property(parameterExpression, property.Name);
                ConstantExpression constantExpression = Expression.Constant(queryValue);
                BinaryExpression binaryExpression = Expression.Equal(propertyExpression, constantExpression);
                Expression<Func<Customer, bool>> lambdaExpression = Expression.Lambda<Func<Customer, bool>>(binaryExpression, parameterExpression);

                return Expression.Lambda<Func<Customer, bool>>(Expression.AndAlso(current.Body, lambdaExpression.Body), parameterExpression);
            });

            return _customerDao.GetAllAsync()
                .Where(filterExpression.Compile())
                .Skip(query.PageIndex * query.PageSize)
                .Take(query.PageSize)
                .Select(customer => customer.ToDto<Customer, CustomerBasicInfoDto>());
        }

        public async Task<BasicInfoDto<Customer>?> GetByIdAsync(object id)
        {
            Customer? customer = await _customerDao.GetByIdAsync(id);
            if (customer == null) return null;
            return customer.ToDto<Customer, CustomerBasicInfoDto>();
        }

        public async Task<BasicInfoDto<Customer>> AddAsync(CreateDto<Customer> newRecord)
        {
            if (newRecord.GetType() != _inputTypes[HttpMethod.Post]) throw new ArgumentException("Invalid type");

            Customer customer = newRecord.ToEntity();
            await _customerDao.AddAsync(customer);
            return customer.ToDto<Customer, CustomerBasicInfoDto>();
        }

        public async Task<BasicInfoDto<Customer>?> UpdateAsync(UpdateDto<Customer> newInfo, object id)
        {
            if (newInfo.GetType() != _inputTypes[HttpMethod.Put]) throw new ArgumentException("Invalid type");

            Customer? customer = await _customerDao.GetByIdAsync(id);
            if (customer == null) throw new KeyNotFoundException("Customer not found");

            await _customerDao.UpdateAsync(customer, newInfo);
            return customer.ToDto<Customer, CustomerBasicInfoDto>();
        }

        public async Task DeleteAsync(object id)
        {
            Customer? customer = await _customerDao.GetByIdAsync(id);
            if (customer == null) throw new KeyNotFoundException("Customer not found");

            await _customerDao.DeleteAsync(customer);
        }

        // public async Task CascadeDeleteAsync(int id)
        // {
        //     Customer? customer = _dbContext.Customers.Find(id);
        //     if (customer == null) throw new KeyNotFoundException("Customer not found");
        //     _dbContext.Customers.Remove(customer);
        //     _dbContext.SaveChangesAsync();
        // }
    }
}

/*  fail: Microsoft.EntityFrameworkCore.Update[10000]
        An exception occurred in the database while saving changes for context type 'Api.AppDbContext'.
        Microsoft.EntityFrameworkCore.DbUpdateException: Could not save changes. Please configure your entity type accordingly.
        ---> MySql.Data.MySqlClient.MySqlException (0x80004005): Cannot delete or update a parent row: a foreign key constraint fails (`test_schema`.`donhang`, CONSTRAINT `fk_khachhang` FOREIGN KEY (`KhachhangID`) REFERENCES `khachhang` (`KhachhangID`))
*/