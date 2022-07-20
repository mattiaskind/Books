using Books.Interfaces;

namespace Books.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }
    }
}
