using Books.Dtos;
using Books.Interfaces;
using Books.Models;

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

        public async Task<bool> UpdateAuthorAsync(int id, CreateAuthorDto authorDto)
        {
            Author? existingAuthor = await _authorRepository.GetAuthorByIdAsync(id);
            if (existingAuthor == null) return false;

            existingAuthor.changeName(authorDto.Name);
            return await _authorRepository.UpdateAuthorAsync(existingAuthor);
        }

        public async Task<ViewAuthorDto> CreateAuthorAsync(CreateAuthorDto authorDto)
        {
            if (authorDto == null) throw new ArgumentException(nameof(authorDto));
            if (await _authorRepository.AuthorNameExistsAsync(authorDto.Name)) throw new ArgumentException("Författaren finns redan");

            var result = await _authorRepository.CreateAuthorAsync(new Author(authorDto.Name));

            return new ViewAuthorDto
            {
                Id = result.Id,
                Name = result.Name
            };

        }

        public async Task<bool> CheckIfAuthorExistsAsync(int id)
        {
            return await _authorRepository.AuthorExistsAsync(id);
        }

        public async Task<bool> CheckIfAuthorNameExists(string name)
        {
            return await _authorRepository.AuthorNameExistsAsync(name);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author is null) return false;
            return await _authorRepository.DeleteAuthorAsync(author);
        }
    }
}
