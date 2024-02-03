using AutoMapper;

namespace CoreBusiness;

public class ProductMapperConfig : IProductMapperConfig
{
    public Mapper Configure()
    {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, Product>()
        );

        var mapper = new Mapper(config);
        return mapper;
    }
}