using AutoMapper;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Domain.Entities.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, RentalDto>()
                .ForMember(
                    r => r.EquipmentId,
                    opt => opt.MapFrom(x => x.Equipment.Id))
                .ForMember(
                    r => r.EquipmentName,
                    opt => opt.MapFrom(x => x.Equipment.Name))
                .ForMember(
                    r => r.Status,
                    opt => opt.MapFrom(x => x.Status.Value.ToString()))
                .ForMember(
                    r => r.Time,
                    opt => opt.MapFrom(x => x.Time.Start));
        }
    }
}
