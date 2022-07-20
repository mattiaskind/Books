namespace Books.Dtos
{
    public class ViewBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public IEnumerable<string> Authors { get; set; } = new List<string>();

    }
}