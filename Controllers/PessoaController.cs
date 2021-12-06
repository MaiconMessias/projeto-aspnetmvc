using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using primeira_projeto_aspnetmvc.Models;
using Microsoft.Extensions.Configuration;

namespace primeira_projeto_aspnetmvc.Controllers
{
    public class PessoaController : Controller
    {
        private readonly Context _context;
        private readonly IConfiguration Configuration;        

        public PessoaController(Context context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;                        
        }
        public PaginatedList<Pessoa> Pessoas { get; set; }
        // GET: Pessoa
        public async Task<IActionResult> Index(string sortOrder, string searchString,
                                               string currentFilter, int? pageIndex)
        {
            IQueryable<Pessoa> pessoaIQ = from s in _context.Pessoas
                                                            select s;
            
            ViewBag.CurrentFilter = searchString;
            
            // Filtro
            if (!String.IsNullOrEmpty(searchString))                                                              
            {
                pessoaIQ = pessoaIQ.Where(s => s.Nome.ToUpper().Contains(searchString.ToUpper()));
            }
            
            // Ordenação colunas
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewBag.CpfSortParam = sortOrder == "CPF" ? "cpf_desc" : "CPF";
            ViewBag.EndeSortParam = sortOrder == "Endereço" ? "endereco_desc" : "Endereço";

            switch (sortOrder)
            {
                case "nome_desc":
                      pessoaIQ = pessoaIQ.OrderByDescending(s => s.Nome);
                      ViewBag.iconOrderNome = "glyphicon glyphicon-menu-down";
                      break;
                case "cpf_desc":      
                      pessoaIQ = pessoaIQ.OrderByDescending(s => s.Cpf);
                      ViewBag.iconOrderCpf = "glyphicon glyphicon-menu-down";                      
                      break;
                case "CPF":      
                      pessoaIQ = pessoaIQ.OrderBy(s => s.Cpf);
                      ViewBag.iconOrderCpf = "glyphicon glyphicon-menu-up";
                      break;
                case "Endereço":      
                      pessoaIQ = pessoaIQ.OrderBy(s => s.Endereco.Logradouro);
                      ViewBag.iconOrderEnde = "glyphicon glyphicon-menu-up";
                      break;
                case "endereco_desc":      
                      pessoaIQ = pessoaIQ.OrderByDescending(s => s.Endereco.Logradouro);
                      ViewBag.iconOrderEnde = "glyphicon glyphicon-menu-down";
                      break;
                default:
                      pessoaIQ = pessoaIQ.OrderBy(s => s.Nome);
                      ViewBag.iconOrderNome = "glyphicon glyphicon-menu-up";
                      break;
            }

            var context = pessoaIQ.Include(p => p.Endereco);
            //var context = _context.Pessoas.Include(p => p.Endereco);
            // Paginação
            var pageSize = Configuration.GetValue("PageSize", 4);
            ViewBag.Pessoas = await PaginatedList<Pessoa>.CreateAsync(
                                                 context.AsNoTracking(), pageIndex ?? 1, pageSize);
            return View(await PaginatedList<Pessoa>.CreateAsync(
                                                 context.AsNoTracking(), pageIndex ?? 1, pageSize));
        }

        // GET: Pessoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoa/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Logradouro");
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,EnderecoId")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Logradouro", pessoa.EnderecoId);
            return View(pessoa);
        }

        // GET: Pessoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Logradouro", pessoa.EnderecoId);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,EnderecoId")] Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Logradouro", pessoa.EnderecoId);
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
