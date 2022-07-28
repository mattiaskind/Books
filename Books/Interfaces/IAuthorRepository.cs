using Books.Models;

namespace Books.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author?> GetAuthorByNameAsync(string name);
        Task<int> GetNumberOfBooksAsync(int id);
        Task<Author> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(Author author);
        Task<bool> AuthorExistsAsync(int id);
        Task<bool> AuthorNameExistsAsync(string name);
        Task<bool> SaveAsync();
    }
}