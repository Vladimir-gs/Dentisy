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
    public class ArchivosPacientesController : ControllerBase
    {
        private readonly DentisyContext _context;

        public ArchivosPacientesController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/ArchivosPacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArchivosPacientes>>> GetArchivosPacientes()
        {
            return await _context.ArchivosPacientes.ToListAsync();
        }

        // GET: api/ArchivosPacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArchivosPacientes>> GetArchivosPacientes(int id)
        {
            var archivosPacientes = await _context.ArchivosPacientes.FindAsync(id);

            if (archivosPacientes == null)
            {
                return NotFound();
            }

            return archivosPacientes;
        }

        // PUT: api/ArchivosPacientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArchivosPacientes(int id, ArchivosPacientes archivosPacientes)
        {
            if (id != archivosPacientes.IdArchivo)
            {
                return BadRequest();
            }

            _context.Entry(archivosPacientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchivosPacientesExists(id))
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

        // POST: api/ArchivosPacientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArchivosPacientes>> PostArchivosPacientes(ArchivosPacientes archivosPacientes)
        {
            _context.ArchivosPacientes.Add(archivosPacientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArchivosPacientes", new { id = archivosPacientes.IdArchivo }, archivosPacientes);
        }

        // DELETE: api/ArchivosPacientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArchivosPacientes(int id)
        {
            var archivosPacientes = await _context.ArchivosPacientes.FindAsync(id);
            if (archivosPacientes == null)
            {
                return NotFound();
            }

            _context.ArchivosPacientes.Remove(archivosPacientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArchivosPacientesExists(int id)
        {
            return _context.ArchivosPacientes.Any(e => e.IdArchivo == id);
        }
    }
}
