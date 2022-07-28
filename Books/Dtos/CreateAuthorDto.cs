using System.ComponentModel.DataAnnotations;

namespace Books.Dtos
{
    public class CreateAuthorDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
