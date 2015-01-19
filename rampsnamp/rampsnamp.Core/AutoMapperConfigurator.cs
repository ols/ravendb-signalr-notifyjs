using AutoMapper;

namespace rampsnamp.Core
{
    public static class AutoMapperConfigurator
    {
        public static void InitializeAutoMapper()
        {
            Mapper.CreateMap<User, UserDto>();
        }
    }
}