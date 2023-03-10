using Blog.Data;
using Blog.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        using var context = new BlogDataContext();
        // context.Users.Add(new User
        // {
        //     Bio = "9x Microsoft MVP",
        //     Email = "andre@balta.io",
        //     Image = "https://balta.io",
        //     Name = "André Baltieri",
        //     PasswordHash = "1234", 
        //     Slug = "andre-baltieri"
        // });
        // context.SaveChanges();

        var user = context.Users.FirstOrDefault();
        var post = new Post
        {
            Author = user,
            Body = "Meu Artigo",
            Category = new Category
            {
                Name = "Backend",
                Slug = "backend"
            },
            CreateDate = System.DateTime.Now,
            //LastUpdate =
            Slug = "meu-artigo",
            Summary = "Neste artigo vamos conferir...",
            //Tag = null,
            Title = "Meu artigo"
        };

        context.Posts.Add(post);
        context.SaveChanges();
    }
}