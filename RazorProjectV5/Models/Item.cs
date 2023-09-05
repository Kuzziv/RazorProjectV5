using System.ComponentModel.DataAnnotations;

namespace RazorProjectV5.Models
{
    public class Item
    {
        [Display(Name = "Item ID")]
        [Required(ErrorMessage = "Item ID is required")]
        [Range(1, 10000, ErrorMessage = "Item ID must be between 1 and 10000")]
        public int Id { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Item Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [Display(Name = "Item Price")]
        [Required(ErrorMessage = "Item Price is required")]
        [Range(1, 10000, ErrorMessage = "Item Price must be between 1 and 10000")]
        public double Price { get; set; }

        public Item(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Item()
        {

        }


    }
}
