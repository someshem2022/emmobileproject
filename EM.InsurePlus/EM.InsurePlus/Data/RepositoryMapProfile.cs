using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using DO =  EM.InsurePlus.Models.DataModels;
using SO = EM.InsurePlus.Models.ServiceModels;

namespace EM.InsurePlus.Data
{
    public class RepositoryMapProfile : Profile
    {
        public RepositoryMapProfile()
        {
        //    CreateMap<SO.VehiclePolicy, DO.VehiclePolicy>(MemberList.None)                       
        //                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
        //                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
        //                .ForMember(d => d.Nic, opt => opt.MapFrom(s => s.Nic))
        //                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address))
        //                .ForMember(d => d.Make, opt => opt.MapFrom(s => s.Make))
        //                .ForMember(d => d.Model, opt => opt.MapFrom(s => s.Model))
        //                .ForMember(d => d.Color, opt => opt.MapFrom(s => s.Color))
        //                .ForMember(d => d.LicencePlate, opt => opt.MapFrom(s => s.LicencePlate))
        //                .ReverseMap();


            //CreateMap<SO.VehicleColor, DO.VehicleColor>(MemberList.None)
            //       .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            //       .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
            //       .ForMember(d => d.Code, opt => opt.MapFrom(s => s.Code));

            //CreateMap<SO.UserType, DO.UserType>(MemberList.None)
            //       .ForMember(d => d.UserTypeId, opt => opt.MapFrom(s => s.Id))
            //       .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));

        }


    }
}
