using AutoMapper;
using EM.InsurePlus.Api.Models;

namespace EM.InsurePlus.Api
{
    using SO = EM.InsurePlus.Services.Models;

    public class ServiceMapProfile : Profile
    {
        public ServiceMapProfile()
        {
            CreateMap<UserModel, SO.UserModel>(MemberList.None)
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Password, opt => opt.MapFrom(s => s.Password))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(d => d.ProfileImage, opt => opt.MapFrom(s => s.ProfileImage))
                .ReverseMap();

            CreateMap<VehiclePolicy, SO.VehiclePolicy>(MemberList.None)
             .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
             .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
             .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
             .ForMember(d => d.Nic, opt => opt.MapFrom(s => s.Nic))
             .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
             .ForMember(d => d.Make, opt => opt.MapFrom(s => s.Make))
             .ForMember(d => d.Model, opt => opt.MapFrom(s => s.Model))
             .ForMember(d => d.Color, opt => opt.MapFrom(s => s.Color))
             .ForMember(d => d.LicencePlate, opt => opt.MapFrom(s => s.LicencePlate))
             .ReverseMap();
        }
    }
}
