using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroceryStoreAPI;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IGroceryStoreService _service;

        public CustomersController(IGroceryStoreService groceryStoreService)
        {
            _service = groceryStoreService;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
               return _service.GetAllCustomers();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
               var tmpCustomer = _service.GetCustomerById(id);

               if (tmpCustomer == null)
                    return NotFound();

               return Ok(tmpCustomer);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.SaveCustomer(customer);

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }
    }
}