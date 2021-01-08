using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingAppApi.Entity;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.Customer.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var customer = await _context.Customer
                // .Include(x => x.ShoppingBracket)
                // .ThenInclude(x => x.GoodsList)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Login(string username, string password)
        {
            Customer user =
                _context.Customer.FirstOrDefault(x => x.Account == username && x.Password == password);
            if (user == null)
            {
                return BadRequest("登录失败");
            }
            else
            {
                return user;
            }
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> Register(string username, string password)
        {
            var customer = new Customer
            {
                Account = username,
                Created = DateTime.Now,
                Id = Guid.NewGuid(),
                NickName = null,
                Password = password,
                Score = 0
            };
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new {id = customer.Id}, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        private bool CustomerExists(Guid id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}