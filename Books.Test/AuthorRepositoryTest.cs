using Books.Data;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Test
{
    [TestClass]
    public class AuthorRepositoryTest
    {
        private readonly ApplicationDbContext _context;

        private List<Book> _books = new();

        public AuthorRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task GetAllAuthors()
        {
            await CreateData();
            var repo = new AuthorRepository(_context);

            var result = await repo.GetAllAuthorsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public async Task GetNumberOfBooks_NoBooks()
        {
            await CreateData();
            var author4 = new Author("Författare 4");
            _context.Authors.Add(author4);
            await _context.SaveChangesAsync();

            var repo = new AuthorRepository(_context);
            var result = await repo.GetNumberOfBooksAsync(author4.Id);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public async Task GetNumberOfBooks_TwoBooks()
        {
            await CreateData();

            var repo = new AuthorRepository(_context);
            var result = await repo.GetNumberOfBooksAsync(_books[0].Authors.Select(x => x.Id).FirstOrDefault());

            Assert.AreEqual(2, result);
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