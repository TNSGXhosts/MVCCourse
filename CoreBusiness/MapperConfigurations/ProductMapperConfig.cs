using AutoMapper;

namespace CoreBusiness;

public static class ProductMapperConfig
{
    public static Mapper Configure()
    {
        var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, Product>();
            }
        );

        var mapper = new Mapper(config);
        return mapper;
    }
}