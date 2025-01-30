using Microsoft.EntityFrameworkCore;
using CrudZadigAPI.Models;

namespace CrudZadigAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> optionsDB) : base(optionsDB) { }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder mBuilder)
        {
            mBuilder.Entity<Produto>().Property(produto => produto.Nome)
                .IsRequired()
                .HasMaxLength(255);

            mBuilder.Entity<Produto>().Property(produto => produto.Descricao)
                .HasMaxLength(1000);

            mBuilder.Entity<Produto>().Property(produto => produto.Preco)
                .HasPrecision(18, 2);

            mBuilder.Entity<Produto>().Property(produto => produto.Quantidade)
                .HasDefaultValue(0);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=produtos.db");
            }
        }
    }
}
