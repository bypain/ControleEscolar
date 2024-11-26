using ControleEscolar.Data;
using ControleEscolar.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleEscolar.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var professores = _context.Professores.ToList();
            return View(professores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Professor professor)
        {
            if (ModelState.IsValid)
            {
                professor.Id = Guid.NewGuid();
                _context.Professores.Add(professor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        public IActionResult Edit(Guid id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [HttpPost]
        public IActionResult Edit(Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Professores.Update(professor);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
            if (professor != null)
            {
                _context.Professores.Remove(professor);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
