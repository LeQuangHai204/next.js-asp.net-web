using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Api.Model.Daos
{
    public class CustomerDao : IEntityDao<Customer>
    {
        private readonly AppDbContext _dbContext;

        public CustomerDao(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncEnumerable<Customer> GetAllAsync()
        {
            try
            {
                return _dbContext.Customers.AsAsyncEnumerable();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Database operation failed: " + ex.Message);
            }
        }

        public async Task<Customer?> GetByIdAsync(object id)
        {
            try
            {
                return await _dbContext.Customers.FindAsync(id as int?);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Database operation failed: " + ex.Message);
            }
        }

        public async Task AddAsync(Customer customer)
        {
            try
            {
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Database operation failed: " + ex.Message);
            }
        }

        public async Task<Customer?> UpdateAsync(Customer customer, object modifiedData)
        {
            try
            {
                customer.Merge(modifiedData);
                return await _dbContext.SaveChangesAsync() == 0 ? null : customer;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("SQL UPDATE command execution failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Database operation failed: " + ex.Message);
            }
        }

        public async Task DeleteAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                string message = ex.InnerException
                    is MySqlException sqlEx && sqlEx.Number == 1451
                    ? "Deleting record with existing related records: "
                    : "SQL DELETE command execution failed: ";
                throw new InvalidOperationException(message + ex.Message);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Database operation failed" + ex.Message);
            }
        }
    }
}

// using System.Linq.Expressions;
// using System.Reflection;
// using Microsoft.EntityFrameworkCore;

// using Api.Model.Dtos;

// namespace Api.Model.Daos
// {
//     public class CustomerDao : IEntityDao<Customer>
//     {
//         private readonly AppDbContext _dbContext;
//         private readonly IEntityMapper<Customer> _dataMapper;
//         private static readonly Dictionary<HttpMethod, Type> _inputTypes = new()
//         {
//             {HttpMethod.Get, typeof(CustomerQueryDto)},
//             {HttpMethod.Post, typeof(CustomerCreateDto)},
//             {HttpMethod.Put, typeof(CustomerUpdateDto)}
//         };

//         public CustomerDao(AppDbContext dbContext, IEntityMapper<Customer> dataMapper)
//         {
//             _dbContext = dbContext;
//             _dataMapper = dataMapper;
//         }

//         public IAsyncEnumerable<BasicInfoDto<Customer>> GetAllAsync(EntityQueryDto<Customer> query)
//         {
//             if (query.GetType() != _inputTypes[HttpMethod.Get]) throw new ArgumentException("Invalid type");

//             IEnumerable<PropertyInfo> properties = query
//                 .GetType()
//                 .GetProperties()
//                 .Where(property => property.GetValue(query) != null
//                     && property.Name != "PageIndex"
//                     && property.Name != "PageSize");

//             Expression<Func<Customer, bool>> initialExpression = Expression.Lambda<Func<Customer, bool>>(
//                 Expression.Constant(true),
//                 Expression.Parameter(typeof(Customer), "customer"));

//             Expression<Func<Customer, bool>> filterExpression = properties.Aggregate(initialExpression, (current, property) =>
//             {
//                 object? queryValue = property.GetValue(query);
//                 ParameterExpression parameterExpression = Expression.Parameter(typeof(Customer), "customer");
//                 MemberExpression propertyExpression = Expression.Property(parameterExpression, property.Name);
//                 ConstantExpression constantExpression = Expression.Constant(queryValue);
//                 BinaryExpression binaryExpression = Expression.Equal(propertyExpression, constantExpression);
//                 Expression<Func<Customer, bool>> lambdaExpression = Expression.Lambda<Func<Customer, bool>>(binaryExpression, parameterExpression);

//                 return Expression.Lambda<Func<Customer, bool>>(Expression.AndAlso(current.Body, lambdaExpression.Body), parameterExpression);
//             });

//             return _dbContext.Customers
//                 .Where(filterExpression)
//                 .Skip(query.PageIndex * query.PageSize)
//                 .Take(query.PageSize)
//                 .Select(customer => (CustomerBasicInfoDto)_dataMapper.ToDto<CustomerBasicInfoDto>(customer))
//                 .AsAsyncEnumerable();
//         }

//         public async Task<BasicInfoDto<Customer>?> GetByIdAsync(int id)
//         {
//             Customer? customer = await _dbContext.Customers.FindAsync(id);
//             if (customer == null) return null;
//             return (CustomerBasicInfoDto)_dataMapper.ToDto<CustomerBasicInfoDto>(customer);
//         }

//         public async Task<Customer> AddAsync(CreateDto<Customer> newRecord)
//         {
//             if (newRecord.GetType() != _inputTypes[HttpMethod.Post]) throw new ArgumentException("Invalid type");
//             Customer customer = _dataMapper.ToEntity(newRecord);
//             await _dbContext.Customers.AddAsync(customer);
//             await _dbContext.SaveChangesAsync();
//             return customer;
//         }

//         public async Task<BasicInfoDto<Customer>?> UpdateAsync(UpdateDto<Customer> newInfo, object id)
//         {
//             if (newInfo.GetType() != _inputTypes[HttpMethod.Put]) throw new ArgumentException("Invalid type");

//             Customer? customer = await _dbContext.Customers.FindAsync(id);
//             if (customer == null) throw new ArgumentException("Customer not found");

//             _dataMapper.Merge(customer, newInfo);
//             if (await _dbContext.SaveChangesAsync() == 0) return null;
//             return _dataMapper.ToDto<CustomerBasicInfoDto>(customer) as BasicInfoDto<Customer>;
//         }

//         public async Task DeleteAsync(int id)
//         {
//             Customer? customer = await _dbContext.Customers.FindAsync(id);
//             if (customer == null) throw new ArgumentException("Customer not found");
//             _dbContext.Customers.Remove(customer);
//             await _dbContext.SaveChangesAsync();
//         }

//         // public async Task CascadeDeleteAsync(int id)
//         // {
//         //     Customer? customer = _dbContext.Customers.Find(id);
//         //     if (customer == null) throw new ArgumentException("Customer not found");
//         //     _dbContext.Customers.Remove(customer);
//         //     _dbContext.SaveChangesAsync();
//         // }
//     }
// }

// /*  fail: Microsoft.EntityFrameworkCore.Update[10000]
//         An exception occurred in the database while saving changes for context type 'Api.AppDbContext'.
//         Microsoft.EntityFrameworkCore.DbUpdateException: Could not save changes. Please configure your entity type accordingly.
//         ---> MySql.Data.MySqlClient.MySqlException (0x80004005): Cannot delete or update a parent row: a foreign key constraint fails (`test_schema`.`donhang`, CONSTRAINT `fk_khachhang` FOREIGN KEY (`KhachhangID`) REFERENCES `khachhang` (`KhachhangID`))
// */