using AutoMapper;
using CoreIdentity.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.API.Model.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Role, RoleViewModel>();
            Mapper.CreateMap<UserInRole, UserInRoleViewModel>();
            Mapper.CreateMap<User, UserViewModel>().ForMember(x => x.Roles,
                r => r.MapFrom(o => o.UserRole));
            
            
        }
    }
}
