using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto
{
    public class NotaAcademicaContext : DbContext
    {
        public NotaAcademicaContext(DbContextOptions<NotaAcademicaContext> options) : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; } // Corrected type
    }
}