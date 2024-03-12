using AutoMapper;
using TABP.Api.Dtos.Auth;
using TABP.Application.Users.Login;
using TABP.Application.Users.Register;

namespace TABP.Api.Mapping;

public class AuthProfile : Profile
{
  public AuthProfile()
  {
    CreateMap<LoginRequest, LoginCommand>();
    CreateMap<RegisterRequest, RegisterCommand>();
  }
}