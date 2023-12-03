using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [AllowNull]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [AllowNull]
        [Required]
        [Range(0, 100, ErrorMessage = "Display Order has to be between 0 and 100")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
