using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Banco.Dominio;
using Banco.Infraestrutura.AcessoDados.Contexto;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Banco.WebApp.Controllers
{
    public class CotistasController : Controller
    {
        private readonly BancoContexto _context;

        public CotistasController()
        {
            _context = new BancoContexto();
        }

        // GET: Cotistas
        public async Task<IActionResult> Index()
        {
            IEnumerable<Cotista> cotistas = new List<Cotista>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://bancowebapiwolff.azurewebsites.net");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await client.GetAsync("/api/cotistas");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var serializadoCotistas = await httpResponseMessage.Content.ReadAsStringAsync();
                cotistas = JsonConvert.DeserializeObject<IEnumerable<Cotista>>(serializadoCotistas);
            }
            return View(cotistas);
        }

        // GET: Cotistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotista = await _context.Cotistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotista == null)
            {
                return NotFound();
            }

            return View(cotista);
        }

        // GET: Cotistas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cotistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cpf,Nome")] Cotista cotista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cotista);
        }

        // GET: Cotistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotista = await _context.Cotistas.FindAsync(id);
            if (cotista == null)
            {
                return NotFound();
            }
            return View(cotista);
        }

        // POST: Cotistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cpf,Nome")] Cotista cotista)
        {
            if (id != cotista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotistaExists(cotista.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cotista);
        }

        // GET: Cotistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotista = await _context.Cotistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotista == null)
            {
                return NotFound();
            }

            return View(cotista);
        }

        // POST: Cotistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cotista = await _context.Cotistas.FindAsync(id);
            _context.Cotistas.Remove(cotista);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotistaExists(int id)
        {
            return _context.Cotistas.Any(e => e.Id == id);
        }
    }
}
