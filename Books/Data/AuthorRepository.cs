using Books.Interfaces;
using Books.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Books.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author?> GetAuthorByNameAsync(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<int> GetNumberOfBooksAsync(int id)
        {
            return await _context.Authors
                .Where(x => x.Id == id)
                .SelectMany(x => x.Books.Select(x => x.Id))
                .CountAsync();
        }

        public async Task<bool> AuthorExistsAsync(string authorName)
        {
            return await _context.Authors.AnyAsync(x => x.Name == authorName);
        }

        public async Task<bool> DeleteAuthorAsync(Author author)
        {
            _context.Authors.Remove(author);
            return await SaveAsync();
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