using Books.Dtos;
using Books.Interfaces;

namespace Books.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;


        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<ViewAuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();

            return authors.Select(author =>
                    new ViewAuthorDto
                    {
                        Id = author.Id,
                        Name = author.Name
                    });
        }

        public async Task<ViewAuthorDto?> GetAuthorById(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null) return null;
            return new ViewAuthorDto
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public async Task<int> GetNumberOfBooks(int id)
        {
            return await _authorRepository.GetNumberOfBooksAsync(id);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author is null) return false;
            return await _authorRepository.DeleteAuthorAsync(author);
        }
    }
}
