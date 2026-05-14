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
    public class CitasController : ControllerBase
    {
        private readonly DentisyContext _context;

        public CitasController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citas>>> GetCitas()
        {
            return await _context.Citas.ToListAsync();
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citas>> GetCitas(int id)
        {
            var citas = await _context.Citas.FindAsync(id);

            if (citas == null)
            {
                return NotFound();
            }

            return citas;
        }

        // PUT: api/Citas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitas(int id, Citas citas)
        {
            if (id != citas.IdCita)
            {
                return BadRequest();
            }

            _context.Entry(citas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitasExists(id))
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

        // POST: api/Citas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Citas>> PostCitas(Citas citas)
        {
            _context.Citas.Add(citas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitas", new { id = citas.IdCita }, citas);
        }

        // DELETE: api/Citas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitas(int id)
        {
            var citas = await _context.Citas.FindAsync(id);
            if (citas == null)
            {
                return NotFound();
            }

            _context.Citas.Remove(citas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitasExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
