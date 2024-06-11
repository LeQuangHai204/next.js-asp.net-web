using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Api.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/v0/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IEntityDao<Customer> _customerDao;

        public CustomerController(IEntityDao<Customer> customerDao)
        {
            _customerDao = customerDao;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll([FromQuery] CustomerQueryDto query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IAsyncEnumerable<IEntityDto<Customer>> customers;

            try
            {
                customers = _customerDao.GetAllAsync(query);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "SQL SELECT command execution failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Get all customers failed: " + ex.Message);
            }

            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IEntityDto<Customer>? customer;

            try
            {
                customer = await _customerDao.GetByIdAsync(id);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "SQL SELECT command execution failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Get customer by id failed: " + ex.Message);
            }

            return customer != null ? Ok(customer) : NotFound("Customer not found");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Customer newCustomer;

            try
            {
                newCustomer = await _customerDao.AddAsync(customer);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "SQL CREATE command execution failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Create customer failed: " + ex.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CustomerUpdateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IEntityDto<Customer>? updatedCustomer;

            try
            {
                updatedCustomer = await _customerDao.UpdateAsync(customer, id);
            }
            catch (ArgumentException ex)
            {
                return NotFound("Update customer failed: " + ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "SQL UPDATE command execution failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Update customer failed: " + ex.Message);
            }

            return updatedCustomer != null ? Ok(updatedCustomer) : NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _customerDao.DeleteAsync(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound("Delete customer failed: " + ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,
                    // Check error code for foreign key constraint violation (1451)
                    (ex.InnerException is MySqlException sqlEx && sqlEx.Number == 1451
                    ? "Deleting record with existing related records: "
                    : "SQL DELETE command execution failed: "
                    ) + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Delete customer failed: " + ex.Message);
            }

            return NoContent();
        }
    }
}