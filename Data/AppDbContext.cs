using apiDesafio.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

namespace desafio.Data
{
    //representacao do banco de memoria
    public class AppDbContext : DbContext
    {
        //criar o dbset para as classes da model 
        //para saber que tem que fazer o mapeamento
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductLogModel> ProductLogs { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }

        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: 
               // "Server=ADX-924\\DBAPIDESAFIO;Database=certobanco;User Id=sa;Password=Senha123!;TrustServerCertificate=True;"
                "DataSource=app.db;Cache=Shared"
                );
    }
}

//DataSource=app.db;Cache=Shared
//Server=nomeDoServidor\NomeDaInstancia;Database=nomeDoBancoDeDados;User Id=nomeDoUsuario;Password=senhaDoUsuario;
//Server=ADX-924\\DBAPIDESAFIO;Database=bancoapi;User Id=sa;Password=Senha123!;TrustServerCertificate=True;;
