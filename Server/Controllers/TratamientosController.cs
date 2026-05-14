using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TratamientosController : ControllerBase
    {
        private readonly DentisyContext _context;

        public TratamientosController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/Tratamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tratamientos>>> GetTratamientos()
        {
            return await _context.Tratamientos.ToListAsync();
        }

        // GET: api/Tratamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tratamientos>> GetTratamientos(int id)
        {
            var tratamientos = await _context.Tratamientos.FindAsync(id);

            if (tratamientos == null)
            {
                return NotFound();
            }

            return tratamientos;
        }

        // PUT: api/Tratamientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamientos(int id, Tratamientos tratamientos)
        {
            if (id != tratamientos.IdTratamiento)
            {
                return BadRequest();
            }

            _context.Entry(tratamientos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratamientosExists(id))
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

        // POST: api/Tratamientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tratamientos>> PostTratamientos(Tratamientos tratamientos)
        {
            _context.Tratamientos.Add(tratamientos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTratamientos", new { id = tratamientos.IdTratamiento }, tratamientos);
        }

        // DELETE: api/Tratamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamientos(int id)
        {
            var tratamientos = await _context.Tratamientos.FindAsync(id);
            if (tratamientos == null)
            {
                return NotFound();
            }

            _context.Tratamientos.Remove(tratamientos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TratamientosExists(int id)
        {
            return _context.Tratamientos.Any(e => e.IdTratamiento == id);
        }
    }
}
