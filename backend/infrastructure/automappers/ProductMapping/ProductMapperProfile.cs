using application.cases.Dtos;
using AutoMapper;
using domain.entities;
using infrastructure.persistence.entities;

namespace infrastructure.automappers.ProductMapping
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Products, ProductEntity>();
            CreateMap<ProductEntity, Products>();

            CreateMap<Products, ProductViewDto>();
            // map.ForAllMembers();
        }
    }
}