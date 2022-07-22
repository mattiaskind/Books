using Books.Dtos;
using Books.Interfaces;
using Books.Models;
using Books.Services;
using Moq;

namespace Books.Test
{
    [TestClass]
    public class BookServiceTest
    {
        private readonly BookService _sut;
        private readonly Mock<IBookRepository> _bookRepoMock = new Mock<IBookRepository>();
        private readonly Mock<IAuthorRepository> _authorRepoMock = new Mock<IAuthorRepository>();
        private readonly Mock<ICategoryRepository> _categoryRepoMock = new Mock<ICategoryRepository>();

        public BookServiceTest()
        {
            _sut = new BookService(_authorRepoMock.Object, _bookRepoMock.Object, _categoryRepoMock.Object);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnBook_WhenExisting()
        {
            var id = 1;
            Author author = new Author("Författare");
            Book book = new Book(id, "Titel", "Pocket", new Category("Fantasy"));
            book.AddAuthor(author);
            _bookRepoMock.Setup(x => x.GetBookByIdAsync(id)).ReturnsAsync(book);

            ViewBookDto? bookDto = await _sut.GetBookByIdAsync(id);

            Assert.IsNotNull(bookDto);
            Assert.AreEqual(id, book.Id);

        }
    }
}
