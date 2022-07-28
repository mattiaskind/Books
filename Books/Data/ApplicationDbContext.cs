using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Book>().Navigation(x => x.Authors).Metadata.SetField("_authors");
            builder.Entity<Book>().HasOne(x => x.Category).WithMany();

            builder.Entity<Author>().Navigation(x => x.Books).Metadata.SetField("_books");
        }
    }
}
