using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using EfCoreRelation.Data;
using EfCoreRelation.DTOs.Employee;
using EfCoreRelation.DTOs.ImageDt;
using EfCoreRelation.Entity.Employees;
using EfCoreRelation.Entity.Image;
using EfCoreRelation.Entity.Register;
using EfCoreRelation.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net.WebSockets;
using DataTable = System.Data.DataTable;

namespace EfCoreRelation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDBContext appDBContext;
        private readonly IWebHostEnvironment environment;


        private readonly IMapper mapper;

        public EmployeeController(IWebHostEnvironment environment, AppDBContext appDBContext, IMapper mapper)
        {
            this.environment = environment;
            this.appDBContext = appDBContext;
            this.mapper = mapper;
        }

        //Register Employee
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (register.MobileNumber == null)
            {
                return BadRequest();
            }
            else if(await appDBContext.registers.AnyAsync(u => u.MobileNumber == register.MobileNumber))
            {
                return BadRequest("Mobile Number is Already Exist");

            }

            await appDBContext.registers.AddAsync(register);
            await appDBContext.SaveChangesAsync();
            return Ok(new {message= "Register successdfull" });
        }

        //Login controller
        [HttpPost("login")]
        public async Task<IActionResult> Authentication([FromBody]  Register register)
        {
            if (register == null)
            {
               
                return BadRequest(new { message = "Please provide mail and password" });

            }
            var empl = await appDBContext.registers.FirstOrDefaultAsync
                (x => x.Email == register.Email && x.password == register.password);

            /* var empl = await appDBContext.registers.FirstOrDefaultAsync
                 (x => logis.Email == register.Email && logis.password == register.password);*/


            if (empl == null)
            {
                return BadRequest(new {message= "Email or password incorrect" });
            }
            return Ok(new { message = "Successfull" });

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


           //[Route("api/images")]

         //[HttpPost]
  /*  public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequestDto request)
    {
            // Validation and file processing
           
           

            var image = mapper.Map<Image>(request);
        appDBContext.images.Add(image);
        await appDBContext.SaveChangesAsync();

        var response = mapper.Map<ImageUploadResponseDto>(image);
        return Ok(response);
    }*/


        [HttpPut]
        [Route("api/UploadImages")]
        public async Task<IActionResult> UploadImages(IFormFile formFile,string productCode)
        {
            APIResponse response = new APIResponse();
            try
            { 
                string filePath = getFilePath(productCode);
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                string imagePath = filePath + "//" + ".png";
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(filePath);
                }
                using (FileStream stream = System.IO.File.Create(imagePath))
                {
                    await formFile.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "pass";
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }
            return Ok(response);

        }
        //upload image in database
        [HttpPut("DBUploadImages")]
        public async Task<IActionResult> DbMultipleUploadImage(IFormFileCollection fileCollection,string productCode)
        {
            APIResponse response = new APIResponse();
            int passcount = 0; int errorCount = 0;
            try
            {foreach (var file in fileCollection)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        this.appDBContext.tblProductimages.Add(new TblProductimage()
                        {Productcode=productCode,
                        Productimage=stream.ToArray()

                        });
                        await this.appDBContext.SaveChangesAsync();
                        passcount++;
                    }
                }

            }
            catch(Exception ex)
            {
                errorCount++;
                response.Message = ex.Message;
            }
            response.ResponseCode = 200;
            response.Result = passcount + " File Upload & " + errorCount + "files failed";
            return Ok(response);
        }
        //get multiple image
        /*[HttpGet("GetDBMultiImages")]
        public async Task<IActionResult> getDbMultiImage(string productCode)
        {
            List<string> ImageUrl = new List<string>();

            try
            {
                var productImge = this.appDBContext.tblProductimages.Where(i => i.Productcode == productCode).ToList();
                if(productImge!=null && productImge.Count > 0)
                {
                    productImge.ForEach(i => { ImageUrl.Add(Convert.ToBase64String(i.Productimage)); });
                }
                else
                {
                    return NotFound();
                }

            }
            catch(Exception ex)
            {

            }
            return Ok(ImageUrl);
        }*/








        [HttpGet("GetDBMultiImages")]
        public async Task<IActionResult> getDbMultiImage(int id)
        {
            List<string> ImageUrl = new List<string>();

            try
            {
                var productImge = this.appDBContext.tblProductimages.Where(i => i.Id == id).ToList();
                if (productImge != null && productImge.Count > 0)
                {
                    productImge.ForEach(i => { ImageUrl.Add(Convert.ToBase64String(i.Productimage)); });
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

            }
            return Ok(ImageUrl);
        }




        [HttpGet("DbDownloadImages")]
        public async Task<IActionResult> DownloadImages(string productCode)
        {
            try
            {
                var productImages = await this.appDBContext.tblProductimages.FirstOrDefaultAsync(i => i.Productcode == productCode);
                if (productImages != null)
                {
                    return File(productImages.Productimage, "img/png", productCode + ".png");
                }
                else
                {
                    return NotFound();
                }
            }

            catch(Exception ex)
            {
                return NotFound();
            }
        }


        //db download pdf 
        [HttpGet("DbDownloadPdf")]
        public async Task<IActionResult> DownloadPdf(string productCode)
        {
            try
            {
                var productImages = await this.appDBContext.tblProductimages.FirstOrDefaultAsync(i => i.Productcode == productCode);
                if (productImages != null)
                {
                    return File(productImages.Productimage, "img/pdf", productCode + ".pdf");
                }
                else
                {
                    return NotFound();
                }
            }

            catch (Exception ex)
            {
                return NotFound();
            }
        }




        [NonAction]
        private string getFilePath(string productCode)
        {
return this.environment.WebRootPath + "\\upload\\product" + productCode;
        }

        //get add image at a time
        


        //export excell
        [HttpGet("importExcell")]
        public ActionResult ExportExcell()
        {
            var emData = GetDataTable();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet(emData, "Details");
                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "register.xlsx");
                }
            }
        } 

        [NonAction]

  private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "registers";
            dt.Columns.Add("Id", typeof(Int32));
            dt.Columns.Add("MobileNumber", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("password", typeof(string));
           

            var list = this.appDBContext.registers.ToList();
            if (list.Count > 0)
            {
                list.ForEach(i =>
                {
                    dt.Rows.Add(i.Id, i.MobileNumber, i.Name, i.Email, i.password);

                });
            }

            return dt;
        }























    }
}
