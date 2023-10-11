using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        /*This will set the alias to the DisplayOrder field, for display of error*/
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage = "Display Order must be between 1 to 100 only!!")]
        public int DisplayOrder { get; set; }
        
        // set the default current value on the creation of the object
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
