using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Context;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TratamientosPiezasController : ControllerBase
    {
        private readonly DentisyContext _context;

        public TratamientosPiezasController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/TratamientosPiezas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TratamientosPiezas>>> GetTratamientosPiezas()
        {
            return await _context.TratamientosPiezas.ToListAsync();
        }

        // GET: api/TratamientosPiezas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TratamientosPiezas>> GetTratamientosPiezas(int id)
        {
            var tratamientosPiezas = await _context.TratamientosPiezas.FindAsync(id);

            if (tratamientosPiezas == null)
            {
                return NotFound();
            }

            return tratamientosPiezas;
        }

        // PUT: api/TratamientosPiezas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTratamientosPiezas(int id, TratamientosPiezas tratamientosPiezas)
        {
            if (id != tratamientosPiezas.IdTratamientoPieza)
            {
                return BadRequest();
            }

            _context.Entry(tratamientosPiezas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TratamientosPiezasExists(id))
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

        // POST: api/TratamientosPiezas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TratamientosPiezas>> PostTratamientosPiezas(TratamientosPiezas tratamientosPiezas)
        {
            _context.TratamientosPiezas.Add(tratamientosPiezas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTratamientosPiezas", new { id = tratamientosPiezas.IdTratamientoPieza }, tratamientosPiezas);
        }

        // DELETE: api/TratamientosPiezas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTratamientosPiezas(int id)
        {
            var tratamientosPiezas = await _context.TratamientosPiezas.FindAsync(id);
            if (tratamientosPiezas == null)
            {
                return NotFound();
            }

            _context.TratamientosPiezas.Remove(tratamientosPiezas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TratamientosPiezasExists(int id)
        {
            return _context.TratamientosPiezas.Any(e => e.IdTratamientoPieza == id);
        }
    }
}
