using AutoMapper;
using Locatudo.Domain.Queries.Responses;

namespace Locatudo.Domain.Entities.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, RentalResponse>()
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
