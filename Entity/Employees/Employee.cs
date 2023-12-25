using EfCoreRelation.Entity.AccademicQualificationDetails;
using EfCoreRelation.Entity.Address;
using EfCoreRelation.Entity.WorkExpreanceDetails;
using System.ComponentModel.DataAnnotations;

namespace EfCoreRelation.Entity.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public string FatherName { get; set; }
        [Required]
        public string MotherName { get; set; }
        [Required]
        public string Nid { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string BirthPlace { get; set; }
        [Required]
        public string Religion { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        [Required]

        public string MaritalStatus { get; set; }
        [Required]
        public string Email { get; set; }
        public EmployeeAddress employeeAddresses { get; set; }

        [Required]
        public DateTime InterviewDare { get; set; }
        [Required]
        public string AppliedPost { get; set; }
        [Required]
        public DateTime JoiningDate { get; set; }
        [Required]
        public int ExpectedSelary { get; set; }

        [Required]
        public string AppliedBy { get; set; }
        public List<AccademicQualification> accademicQualifications { get; set; }
        public List<WorkExperience> workExperiences { get; set; }
    }
}
