using AutoMapper;
using EfCoreRelation.Data;
using EfCoreRelation.DTOs.AccademicQualification;
using EfCoreRelation.DTOs.Address;
using EfCoreRelation.DTOs.Employee;
using EfCoreRelation.DTOs.WorkExprenceDetails;
using EfCoreRelation.Entity.AccademicQualificationDetails;
using EfCoreRelation.Entity.Address;
using EfCoreRelation.Entity.Employees;
using EfCoreRelation.Entity.Register;
using EfCoreRelation.Entity.WorkExpreanceDetails;
using EfCoreRelation.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;

namespace EfCoreRelation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDBContext appDBContext;


        private readonly IMapper mapper;

        public EmployeeController(AppDBContext appDBContext, IMapper mapper)
        {
            this.appDBContext = appDBContext;
            this.mapper = mapper;
        }

        //Register Employee
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (register == null)
            {
                return BadRequest();
            }
            await appDBContext.registers.AddAsync(register);
            await appDBContext.SaveChangesAsync();
            return Ok();
        }

        //Login controller
        [HttpPost("login")]
        public async Task<IActionResult> Authentication([FromBody] Register register)
        {
            if (register == null)
            {
                return BadRequest();

            }
            var empl = await appDBContext.registers.FirstOrDefaultAsync
                (x => x.Email == register.Email && x.password == register.password);
            if (empl == null)
            {
                return BadRequest("Email or password incorrect");
            }
            return Ok("Successfull");

        }



        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
        var allData = appDBContext.employees
       //retrive all  address
       .Include(e => e.employeeAddresses)
         .ThenInclude(ex => ex.presentAddresses)
          .Include(e => e.employeeAddresses)
         .ThenInclude(ex => ex.parmanentAddresses)
        //retrive all Accademic Qualification
        .Include(e => e.accademicQualifications)
        //Retrive all Exprience
       .Include(e => e.workExperiences)
       .ToList();
          return Ok(allData);
     
        }

       





        //get Employee Data by ID
        [HttpGet]
        [Route("{id:int}")]
        public  IActionResult FindEmployeeByID(int id)
        {
           var employee = appDBContext.employees
                    .Include(e => e.employeeAddresses)
         .ThenInclude(ex => ex.presentAddresses)
          .Include(e => e.employeeAddresses)
         .ThenInclude(ex => ex.parmanentAddresses)
        //retrive all Accademic Qualification
        .Include(e => e.accademicQualifications)
       
       //Retrive all Exprience
       .Include(e => e.workExperiences)
           

                    .FirstOrDefault(e => e.Id == id);

            if (employee != null)
            {
                return Ok(employee);
                // Handle case where employee with given ID is not found
              
            }
            return NotFound();


        }




        [HttpPost]
        public async Task<IActionResult> PostAllCustomer(EmployeesDto tempCustommer)

        {
            var newEmployee = mapper.Map<Employee>(tempCustommer);
            appDBContext.employees.Add(newEmployee);
            await appDBContext.SaveChangesAsync();
            /*  return Created($"/customer/${newCustomer.Id}", newCustomer);*/
            return Ok(newEmployee);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = appDBContext.employees
                     .Include(e => e.employeeAddresses)
          .ThenInclude(ex => ex.presentAddresses)
           .Include(e => e.employeeAddresses)
          .ThenInclude(ex => ex.parmanentAddresses)
         //retrive all Accademic Qualification
         .Include(e => e.accademicQualifications)  
        //Retrive all Exprience
        .Include(e => e.workExperiences)
            .FirstOrDefault(e => e.Id == id);
      // Remove the employee from the database
            if (employee != null)
            {
                appDBContext.employees.Remove(employee);
                appDBContext.SaveChanges();

                return Ok(employee);
            }
            return NotFound();
           
        }


        //Update by ID

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult DeleteEmployee([FromBody] EmployeesDto tempCustommer, int id)
        {
            var result = appDBContext.employees.Find(id);
            var employee = appDBContext.employees
                    .Include(e => e.employeeAddresses)
         .ThenInclude(ex => ex.presentAddresses)
          .Include(e => e.employeeAddresses)
         .ThenInclude(ex => ex.parmanentAddresses)
        //retrive all Accademic Qualification
        .Include(e => e.accademicQualifications)
         
       //Retrive all Exprience
       .Include(e => e.workExperiences)
           
           .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var newEmployee = mapper.Map<Employee>(tempCustommer);
            appDBContext.employees.Update(newEmployee);
            appDBContext.SaveChanges();
            return Ok(newEmployee);

        }




















    }
}
