using AutoMapper;
using ShopMS.Services.ProductAPI.Model;
using ShopMS.Services.ProductAPI.Model.Dto;

namespace ShopMS.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<ProductDto, Product>().ReverseMap();


            });
            return mappingConfig;

        }
    }
}
