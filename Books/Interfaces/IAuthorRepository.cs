using Books.Models;

namespace Books.Interfaces
{
    public interface IAuthorRepository
    {
        Task<bool> AuthorExists(string authorName);
        Task<Author?> GetAuthorByName(string name);
    }
}