using Books.Dtos;
using Books.Interfaces;
using Books.Models;

namespace Books.Services
{
    public class BookService
    {
        IAuthorRepository _authorRepository;
        IBookRepository _bookRepository;
        ICategoryRepository _categoryRepository;

        public BookService(IAuthorRepository authorRepository, IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ViewBookDto>> GetAllBooksASync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return books.Select(book => new ViewBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Format = book.Format,
                Category = book.Category.CategoryName,
                Authors = book.Authors.Select(x => x.Name),
            }).ToList();
        }

        public async Task<ViewBookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book is null) return null;
            return new ViewBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Format = book.Format,
                Category = book.Category.CategoryName,
                Authors = book.Authors.Select(x => x.Name)
            };
        }

        public async Task<ViewBookDto> CreateBookAsync(CreateBookDto newBookDto)
        {
            if (await _bookRepository.BookExistsAsync(newBookDto.Title)) throw new ArgumentException(nameof(newBookDto));

            Category? category = await _categoryRepository.GetCategoryByNameAsync(newBookDto.Category);
            if (category is null) category = new Category(newBookDto.Category);

            var newBook = new Book(newBookDto.Title, newBookDto.Format, category);
            foreach (string authorName in newBookDto.Authors)
            {
                var author = await _authorRepository.GetAuthorByNameAsync(authorName);
                if (author is null) newBook.AddAuthor(new Author(authorName));
                else newBook.AddAuthor(author);
            }

            var createdBook = await _bookRepository.CreateBookAsync(newBook);

            return new ViewBookDto
            {
                Id = createdBook.Id,
                Title = createdBook.Title,
                Format = createdBook.Format,
                Category = createdBook.Category.CategoryName,
                Authors = createdBook.Authors.Select(x => x.Name),
            };
        }

        public async Task<bool> ChangeBookAsync(int id, CreateBookDto changedBook)
        {
            Book? book = await _bookRepository.GetBookByIdAsync(id);
            if (book is null) return false;

            book.ChangeTitle(changedBook.Title);
            book.ChangeFormat(changedBook.Format);

            Category? category = await _categoryRepository.GetCategoryByNameAsync(changedBook.Category);
            _ = category == null ? book.ChangeCategory(new Category(changedBook.Category)) : book.ChangeCategory(category);

            List<Author> newListOfAuthors = new();
            foreach (var author in changedBook.Authors)
            {
                var existingAuthor = await _authorRepository.GetAuthorByNameAsync(author);
                if (existingAuthor is null)
                {
                    newListOfAuthors.Add(new Author(author));
                }
                else
                {
                    newListOfAuthors.Add(existingAuthor);
                }
            }

            book.UpdateAuthors(newListOfAuthors);

            return await _bookRepository.UpdateBookAsync(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return false;
            return await _bookRepository.DeleteBookAsync(book);
        }

        public async Task<bool> CheckIfBookExistsAsync(string title)
        {
            return await _bookRepository.BookExistsAsync(title);
        }

    }
}

