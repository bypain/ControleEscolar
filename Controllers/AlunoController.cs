using Microsoft.AspNetCore.Mvc;
using ControleEscolar.Data;
using ControleEscolar.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleEscolar.Controllers
{
    public class AlunoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var alunos = _context.Alunos.ToList();
            return View(alunos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.Id = Guid.NewGuid();
                _context.Alunos.Add(aluno);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(Guid id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
    }
}
