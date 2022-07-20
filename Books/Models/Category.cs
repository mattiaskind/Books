namespace Books.Models
{
    public class Category
    {
        public int Id { get; private set; }
        public string CategoryName { get; private set; }

        public Category(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName)) throw new ArgumentException(nameof(categoryName));
            CategoryName = categoryName.Trim();
        }

        public bool ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName)) throw new ArgumentException(nameof(newName));
            string newNameTrimmed = newName.Trim();
            if (newNameTrimmed == CategoryName) return false;
            CategoryName = newNameTrimmed;
            return true;
        }
    }
}