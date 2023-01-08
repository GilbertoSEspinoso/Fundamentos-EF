using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>   //Aqui será feito o mapeamento, é preciso implementar IEntityTypeConfiguration<>
    {
        public void Configure(EntityTypeBuilder<Category> builder) // todas as configurações 
        {
            // table
            builder.ToTable("Category");  // primeiro passo importante, é conf a tabela

            // Primary Key
            builder.HasKey(x => x.Id);

            // Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); // PRIMARY KEY IDENTITY (1,1)

            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired() // gera o NOT NULL
                .HasColumnName("Name") //atributo para alterar o nome 
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80); // esse atributo que define o valor máximo

            // Nota: Isso tudo aqui é básicamente escrever o scrip do SQLserver, só que, através do C#, isso é o FluentMapping!

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // AS propriedades devem seguir essa mesma sequência,se tivesse mais 20/30 campos ia seguir sempre a mesma sequência.

            // Índices
            builder.HasIndex(x => x.Slug, "IX_Category_Slug") //o segundo parâmetro é o nome do index
                .IsUnique();
        }
    }
}