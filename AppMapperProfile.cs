using AutoMapper;
using EfCoreRelation.DTOs;
using EfCoreRelation.DTOs.AccademicQualification;
using EfCoreRelation.DTOs.Address;
using EfCoreRelation.DTOs.Employee;
using EfCoreRelation.DTOs.ImageDt;
using EfCoreRelation.DTOs.RegisterDto;
using EfCoreRelation.DTOs.WorkExprenceDetails;
using EfCoreRelation.Entity;
using EfCoreRelation.Entity.AccademicQualificationDetails;
using EfCoreRelation.Entity.Address;
using EfCoreRelation.Entity.Employees;
using EfCoreRelation.Entity.Image;
using EfCoreRelation.Entity.Register;
using EfCoreRelation.Entity.WorkExpreanceDetails;

namespace EfCoreRelation
{//inherit profile thats using automapper
    public class AppMapperProfile :Profile
    {
        public AppMapperProfile()

        {//here is destination and destination
            CreateMap<TableProductImgDto, TblProductimage>();
            CreateMap<RegisterDto, Register>();
            //CreateMap<ImageUploadRequestDto, Image>();
            //CreateMap<Image, ImageUploadResponseDto>();

            CreateMap<EmployeesDto,Employee >();
            CreateMap<EmployeeAddressDto, EmployeeAddress>();
            CreateMap<PresentAddressDto, PresentAddress>();
            CreateMap<ParmanentAddressDto, ParmanentAddress>();
            //Accademic qualification
            CreateMap<AccadeMicQulificationDto, AccademicQualification>();
          
            //Work Experience 
            CreateMap<WorkExperienceDto, WorkExperience>();
          




        }
    }
}
