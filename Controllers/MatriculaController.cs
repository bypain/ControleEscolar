using ControleEscolar.Data;
using ControleEscolar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ControleEscolar.Controllers
{
    public class MatriculaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatriculaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var matriculas = _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Professor)
                .Include(m => m.Disciplina)
                .ToList();

            return View(matriculas);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["AlunoId"] = new SelectList(await _context.Alunos.ToListAsync(), "Id", "Nome");
            ViewData["ProfessorId"] = new SelectList(await _context.Professores.ToListAsync(), "Id", "Nome");
            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.ToListAsync(), "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Matricula matricula)
        {
            if (ModelState.IsValid)
            {
                _context.Matriculas.Add(matricula);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["AlunoId"] = new SelectList(_context.Alunos, "Id", "Nome", matricula.AlunoId);
            ViewData["ProfessorId"] = new SelectList(_context.Professores, "Id", "Nome", matricula.ProfessorId);
            ViewData["DisciplinaId"] = new SelectList(_context.Disciplinas, "Id", "Nome", matricula.DisciplinaId);
            
            return View(matricula);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var matricula = await _context.Matriculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            ViewData["AlunoId"] = new SelectList(await _context.Alunos.ToListAsync(), "Id", "Nome", matricula.AlunoId);
            ViewData["ProfessorId"] = new SelectList(await _context.Professores.ToListAsync(), "Id", "Nome", matricula.ProfessorId);
            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.ToListAsync(), "Id", "Nome", matricula.DisciplinaId);

            return View(matricula);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Matricula matricula)
        {
            if (id != matricula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Matriculas.Update(matricula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Matriculas.Any(m => m.Id == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["AlunoId"] = new SelectList(await _context.Alunos.ToListAsync(), "Id", "Nome", matricula.AlunoId);
            ViewData["ProfessorId"] = new SelectList(await _context.Professores.ToListAsync(), "Id", "Nome", matricula.ProfessorId);
            ViewData["DisciplinaId"] = new SelectList(await _context.Disciplinas.ToListAsync(), "Id", "Nome", matricula.DisciplinaId);

            return View(matricula);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var matricula = await _context.Matriculas.FirstOrDefaultAsync(m => m.Id == id);
            if (matricula != null)
            {
                _context.Matriculas.Remove(matricula);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

