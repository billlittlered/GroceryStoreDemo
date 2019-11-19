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
    public class OrdersController : ControllerBase
    {
          private readonly IGroceryStoreService _service;

        public OrdersController(IGroceryStoreService service)
        {
               _service = service;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {

               return _service.GetAllOrders();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

               var order = _service.GetCustomerOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

          // GET: api/Orders/5
          [HttpGet]
          public async Task<IActionResult> GetOrderByDate([FromBody] DateTime orderDate)
          {
               if (!ModelState.IsValid)
               {
                    return BadRequest(ModelState);
               }

               var order = _service.GetOrdersByDate(orderDate);

               if (order == null)
               {
                    return NotFound();
               }

               return Ok(order);
          }
     }
}