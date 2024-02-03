using AutoMapper;

namespace CoreBusiness;

public class CategoryMapperConfig : ICategoryMapperConfig
{
    public Mapper Configure()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Category, Category>()
        );

        var mapper = new Mapper(config);
        return mapper;
    }
}