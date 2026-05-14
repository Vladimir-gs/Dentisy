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
    public class PiezasDentalesController : ControllerBase
    {
        private readonly DentisyContext _context;

        public PiezasDentalesController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/PiezasDentales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PiezasDentales>>> GetPiezasDentales()
        {
            return await _context.PiezasDentales.ToListAsync();
        }

        // GET: api/PiezasDentales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PiezasDentales>> GetPiezasDentales(int id)
        {
            var piezasDentales = await _context.PiezasDentales.FindAsync(id);

            if (piezasDentales == null)
            {
                return NotFound();
            }

            return piezasDentales;
        }

        // PUT: api/PiezasDentales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPiezasDentales(int id, PiezasDentales piezasDentales)
        {
            if (id != piezasDentales.IdPieza)
            {
                return BadRequest();
            }

            _context.Entry(piezasDentales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PiezasDentalesExists(id))
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

        // POST: api/PiezasDentales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PiezasDentales>> PostPiezasDentales(PiezasDentales piezasDentales)
        {
            _context.PiezasDentales.Add(piezasDentales);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPiezasDentales", new { id = piezasDentales.IdPieza }, piezasDentales);
        }

        // DELETE: api/PiezasDentales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePiezasDentales(int id)
        {
            var piezasDentales = await _context.PiezasDentales.FindAsync(id);
            if (piezasDentales == null)
            {
                return NotFound();
            }

            _context.PiezasDentales.Remove(piezasDentales);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PiezasDentalesExists(int id)
        {
            return _context.PiezasDentales.Any(e => e.IdPieza == id);
        }
    }
}
