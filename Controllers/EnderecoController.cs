using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using primeira_projeto_aspnetmvc.Models;

namespace primeira_projeto_aspnetmvc.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration Configuration;

        public EnderecoController(Context context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: Endereco
        public async Task<IActionResult> Index(string sortOrder, string searchString,
                                               string currentFilter, int? pageIndex)
        {
            IQueryable<Endereco> enderecoIQ = from s in _context.Enderecos
                                                                select s;

            ViewBag.CurrentFilter = searchString;

            // Filtro
            if (!String.IsNullOrEmpty(searchString))                                                              
            {
                enderecoIQ = enderecoIQ.Where(s => s.Logradouro.ToUpper().Contains(searchString.ToUpper()));
            }
            
            // Ordenação colunas
            ViewBag.CurrentSort = sortOrder;
            ViewBag.LograSortParm = String.IsNullOrEmpty(sortOrder) ? "logradouro_desc" : "";
            ViewBag.NumeroSortParm = sortOrder == "Número" ? "numero_desc" : "Número";
            ViewBag.CepSortParm = sortOrder == "CEP" ? "cep_desc" : "CEP";
            ViewBag.BairroSortParm = sortOrder == "Bairro" ? "bairro_desc" : "Bairro";
            ViewBag.CidadeSortParm = sortOrder == "Cidade" ? "cidade_desc" : "Cidade";
            ViewBag.EstadoSortParm = sortOrder == "Estado" ? "estado_desc" : "Estado";

            switch (sortOrder)
            {
                case "logradouro_desc":
                      enderecoIQ = enderecoIQ.OrderByDescending(s => s.Logradouro);
                      ViewBag.iconOrderLogradouro = "glyphicon glyphicon-menu-down";
                      break;
                case "numero_desc":      
                      enderecoIQ = enderecoIQ.OrderByDescending(s => s.Numero);
                      ViewBag.iconOrderNumero = "glyphicon glyphicon-menu-down";
                      break;
                case "Número":      
                      enderecoIQ = enderecoIQ.OrderBy(s => s.Numero);
                      ViewBag.iconOrderNumero = "glyphicon glyphicon-menu-up";
                      break;
                case "cep_desc":      
                      enderecoIQ = enderecoIQ.OrderByDescending(s => s.Cep);
                      ViewBag.iconOrderCEP = "glyphicon glyphicon-menu-down";
                      break;
                case "CEP":      
                      enderecoIQ = enderecoIQ.OrderBy(s => s.Cep);
                      ViewBag.iconOrderCEP = "glyphicon glyphicon-menu-up";
                      break;
                case "bairro_desc":      
                      enderecoIQ = enderecoIQ.OrderByDescending(s => s.Bairro);
                      ViewBag.iconOrderBairro = "glyphicon glyphicon-menu-down";
                      break;
                case "Bairro":      
                      enderecoIQ = enderecoIQ.OrderBy(s => s.Bairro);
                      ViewBag.iconOrderBairro = "glyphicon glyphicon-menu-up";
                      break;
                case "cidade_desc":      
                      enderecoIQ = enderecoIQ.OrderByDescending(s => s.Cidade);
                      ViewBag.iconOrderCidade = "glyphicon glyphicon-menu-down";
                      break;
                case "Cidade":      
                      enderecoIQ = enderecoIQ.OrderBy(s => s.Cidade);
                      ViewBag.iconOrderCidade = "glyphicon glyphicon-menu-up";
                      break;
                case "estado_desc":      
                      enderecoIQ = enderecoIQ.OrderByDescending(s => s.Estado);
                      ViewBag.iconOrderEstado = "glyphicon glyphicon-menu-down";
                      break;
                case "Estado":      
                      enderecoIQ = enderecoIQ.OrderBy(s => s.Estado);
                      ViewBag.iconOrderEstado = "glyphicon glyphicon-menu-up";
                      break;
                default:
                      enderecoIQ = enderecoIQ.OrderBy(s => s.Logradouro);
                      ViewBag.iconOrderLogradouro = "glyphicon glyphicon-menu-up";
                      break;
            }

            //var context = _context.Pessoas.Include(p => p.Endereco);
            // Paginação
            var pageSize = Configuration.GetValue("PageSize", 4);
            ViewBag.Enderecos = await PaginatedList<Endereco>.CreateAsync(
                                                 enderecoIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            return View(await PaginatedList<Endereco>.CreateAsync(
                                                 enderecoIQ.AsNoTracking(), pageIndex ?? 1, pageSize));
        }

        // GET: Endereco/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // GET: Endereco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Endereco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Logradouro,Numero,Cep,Bairro,Cidade,Estado")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(endereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Endereco/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        // POST: Endereco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logradouro,Numero,Cep,Bairro,Cidade,Estado")] Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endereco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.Id))
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
            return View(endereco);
        }

        // GET: Endereco/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id)
        {
            return _context.Enderecos.Any(e => e.Id == id);
        }
    }
}
