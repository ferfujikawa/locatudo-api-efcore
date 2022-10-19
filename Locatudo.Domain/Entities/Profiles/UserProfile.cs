using AutoMapper;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Domain.Entities.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    r => r.FirstName,
                    opt => opt.MapFrom(x => x.Name.FirstName))
                .ForMember(
                    r => r.LastName,
                    opt => opt.MapFrom(x => x.Name.LastName))
                .ForMember(
                    r => r.Email,
                    opt => opt.MapFrom(x => x.Email.Address));
        }
    }
}
