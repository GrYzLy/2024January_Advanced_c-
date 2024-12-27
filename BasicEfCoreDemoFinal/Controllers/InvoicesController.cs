using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasicEfCoreDemo.Data;
using BasicEfCoreDemo.Models;
using BasicEfCoreDemo;
using Microsoft.Data.SqlClient;

namespace BasicEfCoreDemoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceDbContext _context;
        private readonly ILogger<InvoicesController> _logger;
        public InvoicesController(InvoiceDbContext context, ILogger<InvoicesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices(int page = 1, int pageSize = 10,
            InvoiceStatus? status = null)
        {
            //_logger.LogInformation($"Creating the IQueryable...");

            //var list1 = _context.Invoices.Where(x => status == null || x.Status == status);


            //_logger.LogInformation($"IQueryable created");
            //_logger.LogInformation($"Query the result using IQueryable...");
            //var query1 = list1.OrderByDescending(x => x.InvoiceDate)
            //    .Skip((page - 1) * pageSize)
            //    .Take(pageSize);

            //_logger.LogInformation($"Execute the query using IQueryable");
            //var result1 = await query1.ToListAsync();

            //_logger.LogInformation($"Result created using IQueryable");

            // Use IEnumerable
            _logger.LogInformation($"Creating the IEnumerable...");
            var list2 = _context.Invoices.Where(x => status == null || x.Status == status).AsEnumerable();
            _logger.LogInformation($"IEnumerable created");
            _logger.LogInformation($"Query the result using IEnumerable...");
            var query2 = list2.OrderByDescending(x => x.InvoiceDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            _logger.LogInformation($"Execute the query using IEnumerable");
            var result2 = query2.ToList();
            _logger.LogInformation($"Result created using IEnumerable");

            return result2;
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Invoice>>> SearchInvoices(string search)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }

            var list = await _context.Invoices
                // The below code will throw an exception if the CalculatedTax method is not static
                //.Where(x => (x.ContactName.Contains(search) || x.InvoiceNumber.Contains(search)) && CalculateTax(x.Amount) > 10)
                .Where(x => (x.ContactName.Contains(search) || x.InvoiceNumber.Contains(search)))
                .Select(x => new Invoice
                {
                    Id = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    ContactName = x.ContactName,
                    // The below conversion will be executed on the client side
                    Description = $"Tax: ${CalculateTax(x.Amount)}. {x.Description}",
                    Amount = x.Amount,
                    InvoiceDate = x.InvoiceDate,
                    DueDate = x.DueDate,
                    Status = x.Status
                })
                .ToListAsync();
            return list;
        }

        private static decimal CalculateTax(decimal amount)
        {
            return amount * 0.15m;
        }


        // GET: api/Invoices/511    
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(Guid id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }

            _logger.LogInformation($"Invoice {id} is loading from the db");
            var invoice = await _context.Invoices.FindAsync(id);
            _logger.LogInformation($"Invoice {invoice?.Id} is loaded from the db");

            _logger.LogInformation($"Invoice {id} is loading from the context");
            invoice = await _context.Invoices.FindAsync(id);
            _logger.LogInformation($"Invoice {invoice?.Id} is loaded from the context");

            //invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            invoice = await _context.Invoices.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }


        [HttpGet]
        [Route("free-search")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices(string propertyName, string propertyValue)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }

            var value = new SqlParameter("value", propertyValue);

            var list = await _context.Invoices
                .FromSqlRaw($"SELECT * FROM Invoices WHERE {propertyName} = @value", value)
                .ToListAsync();
            return list;
        }



        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(Guid id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route(nameof(GetInvoicesByStatus))]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByStatus(int page = 1, int pageSize = 10, InvoiceStatus? status = null)
        {
            var result = _context.Invoices.Where(x => status == null || x.Status == status)
                .OrderByDescending(x => x.InvoiceDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return await result.ToListAsync();
        }
    }
}
