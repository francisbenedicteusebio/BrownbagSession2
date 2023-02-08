using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrownbagSession2.DTO;
using BrownbagSession2.Data;

namespace BrownbagSession2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyCodesDtoesController : ControllerBase
    {
        private readonly CurrencyExchangeContext _context;

        public CurrencyCodesDtoesController(CurrencyExchangeContext context)
        {
            _context = context;
        }

        // GET: api/CurrencyCodesDtoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyCodesDto>>> GetCurrencyCodes()
        {
          if (_context.CurrencyCodes == null)
          {
              return NotFound();
          }
          return await _context.CurrencyCodes.ToListAsync();
        }

        // GET: api/CurrencyCodesDtoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyCodesDto>> GetCurrencyCodesDto(int id)
        {
          if (_context.CurrencyCodes == null)
          {
              return NotFound();
          }
          var currencyCodesDto = await _context.CurrencyCodes.FindAsync(id);

          if (currencyCodesDto == null)
          {
              return NotFound();
          }

          return currencyCodesDto;
        }

        // PUT: api/CurrencyCodesDtoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrencyCodesDto(int id, CurrencyCodesDto currencyCodesDto)
        {
            if (id != currencyCodesDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(currencyCodesDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyCodesDtoExists(id))
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

        // POST: api/CurrencyCodesDtoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CurrencyCodesDto>> PostCurrencyCodesDto(CurrencyCodesDto currencyCodesDto)
        {
          if (_context.CurrencyCodes == null)
          {
              return Problem("Entity set 'CurrencyExchangeContext.CurrencyCodes'  is null.");
          }
          _context.CurrencyCodes.Add(currencyCodesDto);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetCurrencyCodesDto", new { id = currencyCodesDto.Id }, currencyCodesDto);
        }

        // DELETE: api/CurrencyCodesDtoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyCodesDto(int id)
        {
            if (_context.CurrencyCodes == null)
            {
                return NotFound();
            }
            var currencyCodesDto = await _context.CurrencyCodes.FindAsync(id);
            if (currencyCodesDto == null)
            {
                return NotFound();
            }

            _context.CurrencyCodes.Remove(currencyCodesDto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrencyCodesDtoExists(int id)
        {
            return (_context.CurrencyCodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
