namespace Api.Models
{
    public class CustomerDao : IEntityDao<Customer>
    {
        private readonly AppDbContext _dbContext;
        private readonly CustomerMapper _dataMapper;

        public CustomerDao(AppDbContext dbContext, CustomerMapper dataMapper)
        {
            _dbContext = dbContext;
            _dataMapper = dataMapper;
        }

        public IEnumerable<IEntityDto<Customer>> GetAllAsync()
        {
            return _dbContext.Customers
                .Select(customer => (CustomerBasicInfoDto)_dataMapper.ToDto<CustomerBasicInfoDto>(customer))
                .ToList();
        }

        public async Task<IEntityDto<Customer>?> GetByIdAsync(int id)
        {
            Customer? customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null) return null;
            return (CustomerBasicInfoDto)_dataMapper.ToDto<CustomerBasicInfoDto>(customer);
        }

        public async Task<Customer> AddAsync(IEntityDto<Customer> entity)
        {
            if (entity is not CustomerCreateDto dto)
            {
                throw new ArgumentException("Invalid type");
            }

            Customer customer = _dataMapper.ToEntity(dto);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task UpdateAsync(IEntityDto<Customer> entity, object id)
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