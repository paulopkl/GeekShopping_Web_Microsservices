using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.DB.Model;

namespace GeekShopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartVO, Cart>().ReverseMap();

                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();

                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();

                config.CreateMap<ProductVO, Product>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
