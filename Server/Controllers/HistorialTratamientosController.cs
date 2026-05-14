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
    public class HistorialTratamientosController : ControllerBase
    {
        private readonly DentisyContext _context;

        public HistorialTratamientosController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/HistorialTratamientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialTratamientos>>> GetHistorialTratamientos()
        {
            return await _context.HistorialTratamientos.ToListAsync();
        }

        // GET: api/HistorialTratamientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialTratamientos>> GetHistorialTratamientos(int id)
        {
            var historialTratamientos = await _context.HistorialTratamientos.FindAsync(id);

            if (historialTratamientos == null)
            {
                return NotFound();
            }

            return historialTratamientos;
        }

        // PUT: api/HistorialTratamientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialTratamientos(int id, HistorialTratamientos historialTratamientos)
        {
            if (id != historialTratamientos.IdHistorialTratamiento)
            {
                return BadRequest();
            }

            _context.Entry(historialTratamientos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialTratamientosExists(id))
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

        // POST: api/HistorialTratamientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialTratamientos>> PostHistorialTratamientos(HistorialTratamientos historialTratamientos)
        {
            _context.HistorialTratamientos.Add(historialTratamientos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialTratamientos", new { id = historialTratamientos.IdHistorialTratamiento }, historialTratamientos);
        }

        // DELETE: api/HistorialTratamientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialTratamientos(int id)
        {
            var historialTratamientos = await _context.HistorialTratamientos.FindAsync(id);
            if (historialTratamientos == null)
            {
                return NotFound();
            }

            _context.HistorialTratamientos.Remove(historialTratamientos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialTratamientosExists(int id)
        {
            return _context.HistorialTratamientos.Any(e => e.IdHistorialTratamiento == id);
        }
    }
}
