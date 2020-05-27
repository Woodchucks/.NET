using Labo6.Models;
using Microsoft.EntityFrameworkCore;

namespace Labo6.Data
{
    public class Labo6DbContext: DbContext
    {
        public Labo6DbContext(DbContextOptions<Labo6DbContext> options) : base(options)
        {
        }
        public DbSet<Przepis> Przepisy { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        //public DbSet<Produkt> Produkty { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
            //base.OnModelCreating(builder);
            //builder.Entity<PrzepisProdukt>().HasKey(i => new { i.PrzepisId, i.ProduktId });
            //builder.Entity<PrzepisProdukt>().HasOne(bc => bc.Przepis).WithMany(b => b.Produkty).HasForeignKey(bc => bc.PrzepisId);
            //builder.Entity<PrzepisProdukt>().HasOne(bc => bc.Produkt).WithMany(c => c.Przepisy).HasForeignKey(bc => bc.ProduktId);
            
            //wypełnienie tabeli przykładowymi s=daniami, zamiast Seed()
            /*builder.Entity<Kategoria>().HasData(
                new Kategoria
                {
                    Id = 1,
                    Nazwa = "Główna",
                    NadKategoriaId = null
                });*/
        ///}
    }
}
