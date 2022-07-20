using System.ComponentModel.DataAnnotations;

namespace Books.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Format { get; set; } = string.Empty;
        [Required]
        public string Category { get; set; } = string.Empty;
        public IEnumerable<string> Authors { get; set; } = new List<string>();
    }
}