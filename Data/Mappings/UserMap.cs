using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>   //Aqui será feito o mapeamento, é preciso implementar IEntityTypeConfiguration<>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tabela
            builder.ToTable("User");

            //Chave Primária
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();


            //Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            //não foi feito o mapeamento completo nelas    
            builder.Property(x => x.Bio);
            builder.Property(x => x.Email);
            builder.Property(x => x.Image);
            builder.Property(x => x.PasswordHash);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // Índices
            builder.HasIndex(x => x.Slug, "IX_User_Slug") //o segundo parâmetro é o nome do index
                .IsUnique();

            //Relacionamento N para N
            builder
                .HasMany(x => x.Role)
                .WithMany(x => x.User) /*Se eu parece aqui, o ef não iria saber se eu precisava cria uma tabela sociativa, então criamos uma entidade virtual para isso*/
                .UsingEntity<Dictionary<string, object>>( /* esse dicionario é uma matrix de 2 posições, em que o primeiro valor é uma string e o segundo um objeto */
                    "UserRole",
                    role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UserRole_RoleId")
                    .OnDelete(DeleteBehavior.Cascade), // esse foi o mapeamento do Role
               user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_UserRole_UserId")
                    .OnDelete(DeleteBehavior.Cascade)); // esse foi o mapeamento do User
                    // isso vai resultar numa tabela [UserRole]
        }
    }
}