using AutoMapper;
using FunkoProject.Data;

namespace FunkoProject;

public static class AutoMapperConfig
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<DomainToModelMap>();
            cfg.AddProfile<ModelToDomainMap>();
        });
        
        services.AddSingleton(config.CreateMapper());
    }
}