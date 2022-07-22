using Books.Data;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Test
{
    [TestClass]
    public class BookRepositoryTest
    {
        private readonly ApplicationDbContext _context;

        private List<Book> _books = new();

        public BookRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task GetAllBooks_ShouldReturnBooks()
        {
            await CreateData();
            var repo = new BookRepository(_context);
            var result = await repo.GetAllBooksAsync();

            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetAllBooks_NoBooks_ShouldReturnEmptyList()
        {
            var repo = new BookRepository(_context);
            var result = await repo.GetAllBooksAsync();

            Assert.IsInstanceOfType(result, typeof(List<Book>));
            Assert.AreEqual(0, result.Count());
        }

        public async Task CreateData()
        {
            var author1 = new Author("Författare 1"); // Har två böcker
            var author2 = new Author("Författare 2"); // Har en bok
            var author3 = new Author("Författare 3"); // Har ingen bok

            var category1 = new Category("Fantasy");
            var category2 = new Category("Deckare");

            _books = new List<Book>()
            {
                new Book("Bok 1", "Inbunden", category1, new List<Author>() { author1 }),
                new Book("Bok 2", "Inbunden", category2, new List<Author>() { author1 }),
                new Book("Bok 3", "Pocket", category1, new List<Author>() { author2 }),
            };

            _context.Books.AddRange(_books);
            _context.Authors.Add(author3);
            await _context.SaveChangesAsync();
        }
    }
}
