
using System.ComponentModel.DataAnnotations;

namespace EfCoreRelation.Entity.Register
{
    public class Register
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
