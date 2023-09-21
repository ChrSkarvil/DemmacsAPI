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

            //STOCKPRODUCTS
            this.CreateMap<StockProduct, StockProductModel>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.ProductName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Stock.StockAddr));


            this.CreateMap<StockProduct, StockProductModelCreate>()
            .ReverseMap();

            //PRODUCTCOLORS
            this.CreateMap<ProductColor, ProductColorModel>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.ProductName))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color.ColorName));

            this.CreateMap<ProductColor, ProductColorModelCreate>()
            .ReverseMap();

            //ORDERITEMS
            this.CreateMap<Orderitem, OrderItemModel>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product.ProductName));

            this.CreateMap<Orderitem, OrderItemModelCreate>()
            .ReverseMap();

            //CUSTOMERS
            this.CreateMap<Customer, CustomerModel>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.PostalCodeNavigation.City))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.CountryName));

            this.CreateMap<Customer, CustomerModelCreate>()
            .ReverseMap();


            //ORDERS
            this.CreateMap<Order, OrderModel>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.OrderItem.Product.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.OrderItem.Quantity))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payment.PaymentMethod))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.Payment.PaymentDate))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => $"{src.Customer.CustomerFname} {src.Customer.CustomerSname}"))
            .ForMember(dest => dest.DeliveryAddress, opt => opt.MapFrom(src => src.Delivery.DeliveryAddr))
            .ForMember(dest => dest.EstDeliveryDate, opt => opt.MapFrom(src => src.Delivery.EstDeliveryDate))
            .ForMember(dest => dest.DeliveredDate, opt => opt.MapFrom(src => src.Delivery.DeliveredDate))
            .ForMember(dest => dest.DeliveryFee, opt => opt.MapFrom(src => src.Delivery.DeliveryFee))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Delivery.Country.CountryName))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Delivery.PostalCode))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Delivery.PostalCodeNavigation.City));

            this.CreateMap<Order, OrderModelCreate>()
            .ReverseMap();
        }
    }
}
