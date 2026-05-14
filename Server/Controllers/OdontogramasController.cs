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
    public class OdontogramasController : ControllerBase
    {
        private readonly DentisyContext _context;

        public OdontogramasController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/Odontogramas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Odontogramas>>> GetOdontogramas()
        {
            return await _context.Odontogramas.ToListAsync();
        }

        // GET: api/Odontogramas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Odontogramas>> GetOdontogramas(int id)
        {
            var odontogramas = await _context.Odontogramas.FindAsync(id);

            if (odontogramas == null)
            {
                return NotFound();
            }

            return odontogramas;
        }

        // PUT: api/Odontogramas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOdontogramas(int id, Odontogramas odontogramas)
        {
            if (id != odontogramas.IdOdontograma)
            {
                return BadRequest();
            }

            _context.Entry(odontogramas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OdontogramasExists(id))
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

        // POST: api/Odontogramas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Odontogramas>> PostOdontogramas(Odontogramas odontogramas)
        {
            _context.Odontogramas.Add(odontogramas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOdontogramas", new { id = odontogramas.IdOdontograma }, odontogramas);
        }

        // DELETE: api/Odontogramas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOdontogramas(int id)
        {
            var odontogramas = await _context.Odontogramas.FindAsync(id);
            if (odontogramas == null)
            {
                return NotFound();
            }

            _context.Odontogramas.Remove(odontogramas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OdontogramasExists(int id)
        {
            return _context.Odontogramas.Any(e => e.IdOdontograma == id);
        }
    }
}
