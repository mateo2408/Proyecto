using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Controllers
{
    public class NotaAcademicaController : Controller
    {
        private readonly NotaAcademicaContext _context;

        public NotaAcademicaController(NotaAcademicaContext context)
        {
            _context = context;
        }

        // GET: NotaAcademica
        public async Task<IActionResult> Index()
        {
            var grades = await _context.Grades.ToListAsync();
            return View(grades);
        }

        // GET: NotaAcademica/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.ID == id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        // GET: NotaAcademica/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NotaAcademica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Estudiante,Materia,Calificacion,FechaEvaluacion")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: NotaAcademica/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        // POST: NotaAcademica/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Estudiante,Materia,Calificacion,FechaEvaluacion")] Grade grade)
        {
            if (id != grade.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.ID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(grade);
        }

        // GET: NotaAcademica/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var grade = await _context.Grades.FirstOrDefaultAsync(g => g.ID == id);
            if (grade == null) return NotFound();

            return View(grade);
        }

        // POST: NotaAcademica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.ID == id);
        }
    }
}