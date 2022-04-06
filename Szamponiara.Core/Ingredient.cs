using System.ComponentModel.DataAnnotations;

namespace Szamponiara.Core
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Effect Effect { get; set; }
        public string? Notes { get; set; }
    }
}