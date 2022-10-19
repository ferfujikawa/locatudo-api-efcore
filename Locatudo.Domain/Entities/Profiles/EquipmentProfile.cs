using AutoMapper;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Domain.Entities.Profiles
{
    public class EquipmentProfile : Profile
    {
        public EquipmentProfile()
        {
            CreateMap<Equipment, EquipmentDto>()
                .ForMember(
                    r => r.ManagerName,
                    opt => opt.MapFrom(x => (x.Manager != null) ? x.Manager.Name : null));
        }
    }
}
