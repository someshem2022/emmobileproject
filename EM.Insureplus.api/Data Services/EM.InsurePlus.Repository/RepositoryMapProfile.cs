namespace EM.InsurePlus.Repository
{
    using AutoMapper;
    using DO = EM.InsurePlus.Data.Models;
    using IO = EM.InsurePlus.Data.Models.Identity;
    using SO = EM.InsurePlus.Services.Models;

    public class RepositoryMapProfile : Profile
    {
        public RepositoryMapProfile()
        {
            

            CreateMap<SO.UserModel, IO.User>(MemberList.None)
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.ProfileImage, opt => opt.MapFrom(s => s.ProfileImage))
               .ReverseMap();

           

            CreateMap<SO.VehiclePolicy, DO.VehiclePolicy>(MemberList.None)
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

            CreateMap<DO.VehiclePolicy, SO.VehiclePolicy>(MemberList.None)
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