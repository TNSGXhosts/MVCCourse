using AutoMapper;

namespace CoreBusiness;

public static class CategoryMapperConfig
{
    public static Mapper Configure()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Category, Category>()
        );

        var mapper = new Mapper(config);
        return mapper;
    }
}