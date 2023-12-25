using EfCoreRelation.Entity.Address;

namespace EfCoreRelation.DTOs.Address
{
    public class EmployeeAddressDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
    
        public PresentAddressDto presentAddresses { get; set; }
        public ParmanentAddressDto parmanentAddresses { get; set; }
    }
}
