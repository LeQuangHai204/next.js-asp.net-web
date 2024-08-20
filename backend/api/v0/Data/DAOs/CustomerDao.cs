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