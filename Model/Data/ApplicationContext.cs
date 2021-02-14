using Microsoft.EntityFrameworkCore;

namespace CarServiceApp.Model.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Supply> Supplys { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public ApplicationContext()
        {
            //Создание БД, если ее нет.
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CarServiceAppDB;Trusted_Connection=True;");
        } 
    }
}
