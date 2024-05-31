using Microsoft.AspNetCore.Mvc;

using Api.Models;
using Microsoft.VisualBasic;

namespace Api.Controllers
{
    [Route("api/v0/[controller]")]
    [ApiController]
    public class customersController : ControllerBase
    {
        private readonly IEntityDao<Customer> _customerDao;

        public customersController(IEntityDao<Customer> customerDao)
        {
            _customerDao = customerDao;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                IEnumerable<IEntityDto<Customer>> customers = _customerDao.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Get all customers failed: " + ex.Message);
            }
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

            Customer result;
            try
            {
                result = await _customerDao.AddAsync(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Create customer failed: " + ex.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CustomerUpdateDto customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _customerDao.UpdateAsync(customer, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Update customer failed: " + ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _customerDao.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Delete customer failed: " + ex.Message);
            }

            return NoContent();
        }
    }
}