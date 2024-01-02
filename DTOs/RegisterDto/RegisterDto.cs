using System.ComponentModel.DataAnnotations;

namespace EfCoreRelation.DTOs.RegisterDto
{
    public class RegisterDto
    {
        public int Id { get; set; }
    
        public string? MobileNumber { get; set; }
    
        public string? Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
