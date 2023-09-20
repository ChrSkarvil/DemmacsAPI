using AutoMapper;
using DemmacsAPIv2.Data.Entities;
using DemmacsAPIv2.Models;

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
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.UserType == 0 ? src.Customer.CustomerFname : src.Employee.EmployeeFname))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserType == 0 ? "Customer" : src.Employee.Role.RoleName));

            this.CreateMap<Login, LoginModelCreate>()
            .ReverseMap();

            //CATEGORIES
            this.CreateMap<Category, CategoryModel>()
            .ReverseMap();

            //PAYMENTS
            this.CreateMap<Payment, PaymentModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Customer.CustomerFname} {src.Customer.CustomerSname}"));

            this.CreateMap<Payment, PaymentModelCreate>()
            .ReverseMap();

            //CARTS
            this.CreateMap<Cart, CartModel>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => $"{src.Customer.CustomerFname} {src.Customer.CustomerSname}"))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.ProductName));

            this.CreateMap<Cart, CartModelCreate>()
            .ReverseMap();
        }
    }
}
