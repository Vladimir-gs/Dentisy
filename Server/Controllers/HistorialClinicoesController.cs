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
    public class HistorialClinicoesController : ControllerBase
    {
        private readonly DentisyContext _context;

        public HistorialClinicoesController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/HistorialClinicoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialClinico>>> GetHistorialClinico()
        {
            return await _context.HistorialClinico.ToListAsync();
        }

        // GET: api/HistorialClinicoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialClinico>> GetHistorialClinico(int id)
        {
            var historialClinico = await _context.HistorialClinico.FindAsync(id);

            if (historialClinico == null)
            {
                return NotFound();
            }

            return historialClinico;
        }

        // PUT: api/HistorialClinicoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialClinico(int id, HistorialClinico historialClinico)
        {
            if (id != historialClinico.IdHistorial)
            {
                return BadRequest();
            }

            _context.Entry(historialClinico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialClinicoExists(id))
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

        // POST: api/HistorialClinicoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistorialClinico>> PostHistorialClinico(HistorialClinico historialClinico)
        {
            _context.HistorialClinico.Add(historialClinico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialClinico", new { id = historialClinico.IdHistorial }, historialClinico);
        }

        // DELETE: api/HistorialClinicoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialClinico(int id)
        {
            var historialClinico = await _context.HistorialClinico.FindAsync(id);
            if (historialClinico == null)
            {
                return NotFound();
            }

            _context.HistorialClinico.Remove(historialClinico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialClinicoExists(int id)
        {
            return _context.HistorialClinico.Any(e => e.IdHistorial == id);
        }
    }
}
