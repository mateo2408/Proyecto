using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Controllers
{
    [Route("api/Grades")]
    [ApiController]
    public class GradesApiController : ControllerBase
    {
        private readonly NotaAcademicaContext _context;

        public GradesApiController(NotaAcademicaContext context)
        {
            _context = context;
        }

        // GET: api/Grades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            return await _context.Grades.ToListAsync();
        }

        // GET: api/Grades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);

            if (grade == null)
            {
                return NotFound();
            }

            return grade;
        }

        // POST: api/Grades
        [HttpPost]
        public async Task<ActionResult<Grade>> PostGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrade), new { id = grade.ID }, grade);
        }

        // PUT: api/Grades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, Grade grade)
        {
            if (id != grade.ID)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
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

        // DELETE: api/Grades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.ID == id);
        }

        // GET: api/Grades/Student/{student}
        [HttpGet("Student/{student}")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGradesByStudent(string student)
        {
            return await _context.Grades
                .Where(g => g.Estudiante == student)
                .ToListAsync();
        }

        // GET: api/Grades/Subject/{subject}
        [HttpGet("Subject/{subject}")]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGradesBySubject(string subject)
        {
            return await _context.Grades
                .Where(g => g.Materia == subject)
                .ToListAsync();
        }

        // GET: api/Grades/Average/{student}
        [HttpGet("Average/{student}")]
        public async Task<ActionResult<decimal>> CalculateAverage(string student)
        {
            var grades = await _context.Grades
                .Where(g => g.Estudiante == student)
                .ToListAsync();

            if (grades.Count == 0)
            {
                return NotFound("No grades found for this student.");
            }

            decimal average = grades.Average(g => g.Calificacion);
            return average;
        }
    }
}