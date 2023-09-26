using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Store.Entities
{
    public class EBook
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Author name must be between 2 and 50 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Genre  must be between 2 and 50 characters.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category  must be between 2 and 50 characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
       
    }
}