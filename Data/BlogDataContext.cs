using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
     => options.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");

        // a gente precisa informar para o nosso datacontext que temos arquivos de mapeamento, isso é feito no método OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   //aqui eu vou passar a instância do meu mapeamento
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap()); //isso é tudo que a gente precisa para poder rodar nossa aplicação, como criar um bd a partir dela
        }
    }
}