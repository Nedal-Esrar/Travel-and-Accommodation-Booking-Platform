using AutoMapper;
using MediatR;
using TAABB.Application.Users.Login;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Auth;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Users.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;

  public LoginCommandHandler(
    IUserRepository userRepository,
    IJwtTokenGenerator jwtTokenGenerator,
    IMapper mapper)
  {
    _userRepository = userRepository;
    _jwtTokenGenerator = jwtTokenGenerator;
    _mapper = mapper;
  }

  public async Task<LoginResponse> Handle(
    LoginCommand request,
    CancellationToken cancellationToken = default)
  {
    var user = await _userRepository.AuthenticateAsync(request.Email,
                 request.Password, cancellationToken)
               ?? throw new CredentialsNotValidException(UserMessages.CredentialsNotValid);

    return _mapper.Map<LoginResponse>(_jwtTokenGenerator.Generate(user));
  }
}