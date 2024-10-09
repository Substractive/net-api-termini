using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Termin> Termini { get; set; } // Termini, naziv tablice u bazi
        public DbSet<Usluga> Usluge { get; set; } // Usluge, naziv tablice u bazi
        public DbSet<Zaposlenik> Zaposlenici { get; set; } // Zaposlenici, naziv tablice u bazi
    }
}
