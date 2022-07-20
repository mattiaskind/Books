using Books.Interfaces;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorExists(string authorName)
        {
            return await _context.Authors.AnyAsync(x => x.Name == authorName);
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}