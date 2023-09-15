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
            this.CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryName));
        }
    }
}
