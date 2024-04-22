using System.ComponentModel.DataAnnotations;

namespace advisor_backend.Models
{
    public class Advisor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SIN is required")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "SIN must be exactly 9 digits")]
       // [UniqueSIN(ErrorMessage = "SIN must be unique")]
        public string SIN { get; set; }

        [MaxLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
        public string Address { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone must be exactly 8 digits")]
       // [Masked]
        public string Phone { get; set; }

        public string HealthStatus {get; set;}
        
    }
    
}
