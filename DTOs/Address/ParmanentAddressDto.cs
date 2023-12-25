using EfCoreRelation.Entity.Address;
using System.ComponentModel.DataAnnotations;

namespace EfCoreRelation.DTOs.Address
{
    public class ParmanentAddressDto
    {
        public int Id { get; set; }
        [Required]
        public string Village { get; set; }
        [Required]
        public string PostOffice { get; set; }
        [Required]
        public string PoliceStation { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public int EmployeeAddressId { get; set; }
    
    }
}
