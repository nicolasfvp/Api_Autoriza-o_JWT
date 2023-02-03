using ExercicioSemana2.Models;
using Microsoft.EntityFrameworkCore;

namespace ExercicioSemana2.Context
{
    public class RhContext : DbContext
    {
        public RhContext(DbContextOptions<RhContext> options) : base(options) { }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}
