using EfCoreRelation.Entity;
using EfCoreRelation.Entity.AccademicQualificationDetails;
using EfCoreRelation.Entity.Address;
using EfCoreRelation.Entity.Employees;
using EfCoreRelation.Entity.Image;
using EfCoreRelation.Entity.Register;
using EfCoreRelation.Entity.WorkExpreanceDetails;
using Microsoft.EntityFrameworkCore;

namespace EfCoreRelation.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TblProductimage> tblProductimages { get; set; }
       // public DbSet<Image> images { get; set; }
        public DbSet<Register> registers { get; set; }
        public DbSet<Employee> employees { get; set; }
        //all address table 
        public DbSet<EmployeeAddress> employeeAddresses { get; set; }
        public DbSet<PresentAddress> presentAddresses { get; set; }
        public DbSet<ParmanentAddress> parmanentAddresses { get; set; }
        //Accademic qualificatin Table
        public DbSet<AccademicQualification> accademicQualifications { get; set; }
     

        //All work Expreince table added here
       public DbSet<WorkExperience> workExperiences { get; set; }
       



        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            //Accademic qulification table 
            modelBuilder.Entity<AccademicQualification>()
               .HasOne(_ => _.employee)
                .WithMany(_ => _.accademicQualifications)
                .HasForeignKey(_ => _.EmployeeId);


           
         

            //Work Experience MAin Table  
            modelBuilder.Entity<WorkExperience>()
               .HasOne(_ => _.employee)
                .WithMany(_ => _.workExperiences)
                .HasForeignKey(_ => _.EmployeeId);


            

           





















        }


    }





     

    }

