using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banco.Dominio;
using Banco.Infraestrutura.AcessoDados.Contexto;

namespace Banco.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotistasController : ControllerBase
    {
        private readonly BancoContexto _context;

        public CotistasController(BancoContexto context)
        {
            _context = context;
        }

        // GET: api/Cotistas
        [HttpGet]
        public IEnumerable<Cotista> GetCotistas()
        {
            return _context.Cotistas;
        }

        // GET: api/Cotistas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCotista([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cotista = await _context.Cotistas.FindAsync(id);

            if (cotista == null)
            {
                return NotFound();
            }

            return Ok(cotista);
        }

        // PUT: api/Cotistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCotista([FromRoute] int id, [FromBody] Cotista cotista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cotista.Id)
            {
                return BadRequest();
            }

            _context.Entry(cotista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotistaExists(id))
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

        // POST: api/Cotistas
        [HttpPost]
        public async Task<IActionResult> PostCotista([FromBody] Cotista cotista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cotistas.Add(cotista);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCotista", new { id = cotista.Id }, cotista);
        }

        // DELETE: api/Cotistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCotista([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cotista = await _context.Cotistas.FindAsync(id);
            if (cotista == null)
            {
                return NotFound();
            }

            _context.Cotistas.Remove(cotista);
            await _context.SaveChangesAsync();

            return Ok(cotista);
        }

        private bool CotistaExists(int id)
        {
            return _context.Cotistas.Any(e => e.Id == id);
        }
    }
}