using AutoMapper;
using TAABB.Application.Users.Login;
using TABP.Application.Users.Register;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class UsersProfile : Profile
{
  public UsersProfile()
  {
    CreateMap<JwtToken, LoginResponse>();
    CreateMap<RegisterCommand, User>();
  }
}