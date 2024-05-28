using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class CustomerDao : IDbEntityDao<Customer>
    {
        private readonly AppDbContext _dbContext;
        private readonly CustomerDtoMapper _dataMapper;

        public CustomerDao(AppDbContext dbContext, CustomerDtoMapper dataMapper)
        {
            _dbContext = dbContext;
            _dataMapper = dataMapper;
        }

        public IAsyncEnumerable<IDbEntityDto<Customer>> GetAllAsync()
        {
            return _dbContext.Customers
                .AsAsyncEnumerable()
                .Select(customer => _dataMapper.ConvertEntityToBasicInfoDto(customer));
        }

        public async Task<IDbEntityDto<Customer>?> GetByIdAsync(int id)
        {
            Customer? customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null) return null;
            return _dataMapper.ConvertEntityToBasicInfoDto(customer);
        }

        public async Task<Customer> AddAsync(IDbEntityDto<Customer> entity)
        {
            if (entity is not CustomerCreateDto dto)
            {
                throw new ArgumentException("Invalid type");
            }

            Customer customer = _dataMapper.ConvertCreateRequestDtoToEntity(dto);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }
        public async Task UpdateAsync(IDbEntityDto<Customer> entity, object id)
        {
            if (entity is not CustomerUpdateDto newInfo)
            {
                throw new ArgumentException("Invalid type");
            }

            Customer? customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null) throw new ArgumentException("Customer not found");
            _dbContext.Entry(customer).CurrentValues.SetValues(newInfo);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Customer? customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null) throw new ArgumentException("Customer not found");
            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}