using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Dtos.Customers;
using Ticket.Dtos.Roles;
using Ticket.Dtos.Tickets;
using Ticket.Dtos.UserRoles;
using Ticket.Entities.Entities;

namespace Ticket.Core.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customers, CustomerDto>().ReverseMap();
            CreateMap<Roles, RoleDto>().ReverseMap();
            CreateMap<Tickets, TicketDto>().ReverseMap();
            CreateMap<UserRoles, UserRoleDto>().ReverseMap();
        }
    }
}
