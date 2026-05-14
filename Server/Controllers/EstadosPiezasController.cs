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
    public class EstadosPiezasController : ControllerBase
    {
        private readonly DentisyContext _context;

        public EstadosPiezasController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/EstadosPiezas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosPiezas>>> GetEstadosPiezas()
        {
            return await _context.EstadosPiezas.ToListAsync();
        }

        // GET: api/EstadosPiezas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadosPiezas>> GetEstadosPiezas(int id)
        {
            var estadosPiezas = await _context.EstadosPiezas.FindAsync(id);

            if (estadosPiezas == null)
            {
                return NotFound();
            }

            return estadosPiezas;
        }

        // PUT: api/EstadosPiezas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadosPiezas(int id, EstadosPiezas estadosPiezas)
        {
            if (id != estadosPiezas.IdEstadoPieza)
            {
                return BadRequest();
            }

            _context.Entry(estadosPiezas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadosPiezasExists(id))
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

        // POST: api/EstadosPiezas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadosPiezas>> PostEstadosPiezas(EstadosPiezas estadosPiezas)
        {
            _context.EstadosPiezas.Add(estadosPiezas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadosPiezas", new { id = estadosPiezas.IdEstadoPieza }, estadosPiezas);
        }

        // DELETE: api/EstadosPiezas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadosPiezas(int id)
        {
            var estadosPiezas = await _context.EstadosPiezas.FindAsync(id);
            if (estadosPiezas == null)
            {
                return NotFound();
            }

            _context.EstadosPiezas.Remove(estadosPiezas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadosPiezasExists(int id)
        {
            return _context.EstadosPiezas.Any(e => e.IdEstadoPieza == id);
        }
    }
}
