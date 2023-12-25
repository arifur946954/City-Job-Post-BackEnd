



using EfCoreRelation.Entity.Employees;
using System.ComponentModel.DataAnnotations;

namespace EfCoreRelation.Entity.Address
{
    public class EmployeeAddress
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
       
      public Employee employee { get; set; }
        public PresentAddress presentAddresses { get; set; }
        public ParmanentAddress parmanentAddresses { get; set; }



    }
}
