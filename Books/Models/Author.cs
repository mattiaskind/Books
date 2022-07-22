namespace Books.Models
{
    public class Author
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<Book> Books => _books;
        private List<Book> _books;

        public Author(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));
            Name = name;
            _books = new();
        }

        public bool changeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException(nameof(newName));
            if (Name == newName) return false;
            Name = newName;
            return true;
        }

        public bool AddBook(Book book)
        {
            if (book == null) return false;
            if (_books.Any(x => x.Title == book.Title)) return false;
            _books.Add(book);
            return true;
        }
    }
}
