using AutoMapper;
using Cukiernia.Aplication.ApplicationUser;
using Cukiernia.Aplication.Cukiernia;
using Cukiernia.Aplication.Cukiernia.Commands.AddSuBProductToList;
using Cukiernia.Aplication.Cukiernia.Commands.CreateProductQuantity;
using Cukiernia.Aplication.Cukiernia.Commands.DeleteBacking;
using Cukiernia.Aplication.Cukiernia.Commands.EditBaking;
using Cukiernia.Aplication.Cukiernia.Commands.EditSubProduct;
using Cukiernia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Aplication.Mappings
{
    public class CukierniaMappingProfile : Profile
    {
        public CukierniaMappingProfile(IUserContext userContext)
        {
            var user = userContext.GetCurrentUser();

            CreateMap<BakingDto, Domain.Entities.Baking>();
            //.ForMember(e => e.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(e => e.Description, opt => opt.MapFrom(src => src.Description))
            //.ForMember(e => e.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Domain.Entities.Baking, BakingDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => user != null && src.CreatedById == user.Id));


            CreateMap<BakingDto, EditBakingCommand>();
            CreateMap<SubProductDto, EditSubProductCommand>();

            CreateMap<Domain.Entities.SubProduct, SubProductDto>()
                .ReverseMap();

            CreateMap<ProductQuantity, CreateProductQuantityCommand>()
                .ReverseMap();

            CreateMap<ProductQuantity,AddSuBProductToListCommand>()
                .ReverseMap();

            CreateMap<Domain.Entities.ProductQuantity, ProductQuantityDto>()
                .ReverseMap();
        }
    }
}
