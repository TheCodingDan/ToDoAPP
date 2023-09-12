using API.Domain.DTO;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Domain.Entities.Task, TaskDTO>();
            
        }

    }
}