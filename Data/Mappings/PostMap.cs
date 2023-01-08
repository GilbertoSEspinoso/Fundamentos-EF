using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>   //Aqui será feito o mapeamento, é preciso implementar IEntityTypeConfiguration<>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            // Tabela
            builder.ToTable("Post");

            //Chave Primária
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            //Propriedades
            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate") // DATETIME NOT NULL DEFAULT (GETDATE())
                .HasColumnType("SMALLDATETIME")
                .HasDefaultValueSql("GETDATE"); // INFORMA QUE ESSA FUNÇÃO VAI SER EXECUTADA DENTRO DO SQLSERVER
            /* .HasDefaultValue(DateTime.Now.ToUniversalTime()); //usando datetime direto do c# */

            // Índices
            builder
                .HasIndex(x => x.Slug, "IX_Post_Slug")
                .IsUnique();

            //Relacionamentos <<<<<<<<<<<<<<<<<<<<<
            builder
                .HasOne(x => x.Author) //possui um 
                .WithMany(x => x.Posts) //com muitos //essa é uma relação de um p/ muitos
                .HasConstraintName("FK_Post_Author");

            builder
                .HasOne(x => x.Category) //possui um 
                .WithMany(x => x.Posts) //com muitos //essa é uma relação de um p/ muitos
                .HasConstraintName("FK_Post_Category");

            // relação de  muitos para muitos (N N)
            builder.HasMany(x => x.Tags)
                .WithMany(x => x.Post) /*Se eu parece aqui, o ef não iria saber se eu precisava cria uma tabela sociativa, então criamos uma entidade virtual para isso*/
                .UsingEntity<Dictionary<string, object>>( /* esse dicionario é uma matrix de 2 posições, em que o primeiro valor é uma string e o segundo um objeto */
                    "PostTag"
                    , post => post.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("PostId")
                    .HasConstraintName("FK_PostTag_PostId")
                    .OnDelete(DeleteBehavior.Cascade), // esse foi o mapeamento do post
                tag => tag.HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .HasConstraintName("FK_PostTag_TagId")
                    .OnDelete(DeleteBehavior.Cascade)); // esse foi o mapeamento do tag

        }
    }
}