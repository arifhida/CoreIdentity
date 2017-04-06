using AutoMapper;
using CoreIdentity.API.Options;
using CoreIdentity.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Model.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<UserViewModel, User>();
            Mapper.CreateMap<RoleViewModel, Role>();
            Mapper.CreateMap<UserInRoleViewModel, UserInRole>();
            Mapper.CreateMap<CategoryViewModel, Category>();
            Mapper.CreateMap<ProductViewModel, Product>();
            Mapper.CreateMap<ProductAttributeViewModel, ProductAttribute>();                 
        }
    }
}
