using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookIt.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name{ get; set; }
        [DisplayName("Display Name")]
        [Range(1,5000,ErrorMessage ="Display Order must be between 1-5000")]
        public int DisplayOrder { get; set; }
    }
}
