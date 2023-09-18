using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DemmacsAPIv2.Data
{
    public class DemmacsProfile : Profile
    {
        public DemmacsProfile()
        {
            //PRODUCTS
            this.CreateMap<Product, ProductModel>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacture.ManufacturerName));

            this.CreateMap<Product, ProductModelCreate>()
            .ReverseMap();

            //LOGINS
            this.CreateMap<Login, LoginModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Customer.CustomerFname))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Employee.EmployeeFname))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Employee.Role.RoleName));

            this.CreateMap<Login, LoginModelCreate>()
            .ReverseMap();
        }
    }
}
