namespace Books.Models
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Format { get; private set; }
        public Category Category { get; private set; }

        public IReadOnlyList<Author> Authors => _authors;
        private List<Author> _authors;

        private Book() { }

        public Book(string title, string format, Category category)
        {
            Title = title;
            Format = format;
            Category = category;
            _authors = new List<Author>();
        }

        public Book(string title, string format, Category category, List<Author> authors)
        {
            Title = title;
            Format = format;
            Category = category;
            _authors = authors;
        }

        public Book(int id, string title, string format, Category category)
        {
            Id = id;
            Title = title;
            Format = format;
            Category = category;
            _authors = new List<Author>();
        }

        public bool ChangeTitle(string newTitle)
        {
            if (string.IsNullOrEmpty(newTitle)) throw new ArgumentException(nameof(newTitle));
            if (Title == newTitle) return false;
            Title = newTitle;
            return true;
        }

        public bool ChangeFormat(string newFormat)
        {
            if (string.IsNullOrEmpty(newFormat)) throw new ArgumentException(nameof(newFormat));
            if (Format == newFormat) return false;
            Format = newFormat;
            return true;
        }

        public bool ChangeCategory(Category newCategory)
        {
            if (newCategory == null) throw new ArgumentNullException(nameof(newCategory));
            if (Category == newCategory) return false;
            Category = newCategory;
            return true;
        }

        public bool AddAuthor(Author author)
        {
            if (_authors.Any(x => x.Name == author.Name)) return false;
            _authors.Add(author);
            return true;
        }

        public bool AddAuthors(IEnumerable<Author> authors)
        {
            int count = _authors.Count;
            foreach (var author in authors)
            {
                if (!_authors.Any(x => x.Name == author.Name)) AddAuthor(author);
            }
            return _authors.Count > count;
        }

        public bool RemoveAuthorFromBook(Author author)
        {
            return _authors.Remove(author);
        }

        public void UpdateAuthors(IEnumerable<Author> authors)
        {
            _authors.Clear();
            _authors.AddRange(authors);
        }
    }
}
