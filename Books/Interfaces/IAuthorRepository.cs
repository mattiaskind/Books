using Books.Models;

namespace Books.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author?> GetAuthorByNameAsync(string name);
        Task<int> GetNumberOfBooksAsync(int id);
        Task<bool> DeleteAuthorAsync(Author author);
        Task<bool> AuthorExistsAsync(string authorName);
        Task<bool> SaveAsync();
    }
}