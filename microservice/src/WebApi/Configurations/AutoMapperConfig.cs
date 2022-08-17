using AutoMapper;
using Models.Business;
using Models.Mapper.Request;
using Models.Mapper.Response;

namespace WebApi.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Client, ClientPost>()
                .ForMember(c => c.Nome, source => source.MapFrom(source => source.Name))
                .ForMember(c => c.Estado, source => source.MapFrom(source => source.State))
                .ReverseMap()
                .ForMember(d => d.CPF, opt => opt.MapFrom(s => s.CPF.Replace(".", "").Replace("-", "")));

            CreateMap<Client, ClientResponse>()
                .ForMember(c => c.Nome, source => source.MapFrom(source => source.Name))
                .ForMember(c => c.Estado, source => source.MapFrom(source => source.State))
                .ReverseMap();
        }
    }

    public static class AutoMapperExtension
    {
        /// <summary>
        /// Registrar AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            mappingConfig.AssertConfigurationIsValid();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
