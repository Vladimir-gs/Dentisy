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
    public class PacientesController : ControllerBase
    {
        private readonly DentisyContext _context;

        public PacientesController(DentisyContext context)
        {
            _context = context;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pacientes>>> GetPacientes()
        {
            return await _context.Pacientes.ToListAsync();
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pacientes>> GetPacientes(int id)
        {
            var pacientes = await _context.Pacientes.FindAsync(id);

            if (pacientes == null)
            {
                return NotFound();
            }

            return pacientes;
        }

        // PUT: api/Pacientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPacientes(int id, Pacientes pacientes)
        {
            if (id != pacientes.IdPaciente)
            {
                return BadRequest();
            }

            _context.Entry(pacientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacientesExists(id))
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

        // POST: api/Pacientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pacientes>> PostPacientes(Pacientes pacientes)
        {
            _context.Pacientes.Add(pacientes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPacientes", new { id = pacientes.IdPaciente }, pacientes);
        }

        // DELETE: api/Pacientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacientes(int id)
        {
            var pacientes = await _context.Pacientes.FindAsync(id);
            if (pacientes == null)
            {
                return NotFound();
            }

            _context.Pacientes.Remove(pacientes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacientesExists(int id)
        {
            return _context.Pacientes.Any(e => e.IdPaciente == id);
        }
    }
}
