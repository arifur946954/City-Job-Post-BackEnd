using System.ComponentModel.DataAnnotations;

namespace EfCoreRelation.Entity.Address
{
    public class PresentAddress
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
    
        public int EmployeeAddressId { get; set; }
        public EmployeeAddress employeeAddress { get; set; }
    }
}
