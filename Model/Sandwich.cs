using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPISandwich.Model
{
    [Table("Sandwich")]
    public class Sandwich
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public double? Price { get; set; }
    }
}