using Books.Interfaces;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Books.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        private IQueryable<Book> Books => _context.Books
            .Include(x => x.Authors)
            .Include(x => x.Category)
            .AsQueryable();

        public BookRepository(ApplicationDbContext context) => _context = context;
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await SaveAsync();
            return book;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            return await SaveAsync();
        }

        public async Task<bool> DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            return await SaveAsync();
        }

        public async Task<bool> BookExistsAsync(string title)
        {
            return await _context.Books.AnyAsync(x => x.Title == title);
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException)
            {
                return false;
            }
        }
    }
}