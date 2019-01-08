using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly WebApplication1Context _context;
        
        public CurrenciesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: api/Currencies
        [HttpGet]
        public IEnumerable<Currency> GetCurrency()
        {
            return _context.Currency;
        }
        // POST: api/Currencies
        [HttpPost("{id}/{amount}")]
        public async Task<IActionResult> PostCurrency([FromRoute] string id, int amount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currency = _context.Currency.Find(id);

            return Ok(currency.Ratio*amount);
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrency([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currency = await _context.Currency.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return Ok(currency);
        }

        // PUT: api/Currencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrency([FromRoute] string id, [FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != currency.Id)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
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

        // POST: api/Currencies
        [HttpPost]
        public async Task<IActionResult> PostCurrency([FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Currency.Add(currency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrency", new { id = currency.Id }, currency);
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();

            return Ok(currency);
        }

        private bool CurrencyExists(string id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}