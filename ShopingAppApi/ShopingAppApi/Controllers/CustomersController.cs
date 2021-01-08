using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingApp.Share.Dto;
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
        private readonly IMapper _mapper;

        public CustomersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.Customer.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
        {
            var customer = await _context.Customer
                // .Include(x => x.ShoppingBracket)
                // .ThenInclude(x => x.GoodsList)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return _mapper.Map<CustomerDto>(customer);
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
        public async Task<ActionResult<CustomerDto>> PutCustomer(Guid id, CustomerDto customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            var customer = _mapper.Map<Customer>(customerDto);
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

            return _mapper.Map<CustomerDto>(customerDto);
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
        public async Task<ActionResult<CustomerDto>> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        [HttpPost]
        public async Task<ActionResult<List<CustomerDto>>> DeleteManyCustomer(List<Guid> customerIds)
        {
            var customers = await _context.Customer.Where(x => customerIds.Contains(x.Id)).ToListAsync();
            _context.Customer.RemoveRange(customers);

            return _mapper.Map<List<CustomerDto>>(customers);
        }

        [HttpGet]
        private bool CustomerExists(Guid id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<CustomerDto> AddCustomer(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.Created = DateTime.Now;
            customer.Id = Guid.NewGuid();

            await _context.Customer.AddAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}