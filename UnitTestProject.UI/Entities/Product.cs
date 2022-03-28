using System.ComponentModel.DataAnnotations;

namespace UnitTestProject.UI.Entities
{
    public class Product
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
