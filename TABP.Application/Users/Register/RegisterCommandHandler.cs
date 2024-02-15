using AutoMapper;
using MediatR;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Users.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
  private readonly IMapper _mapper;
  private readonly IRoleRepository _roleRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public RegisterCommandHandler(
    IUserRepository userRepository,
    IMapper mapper,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork)
  {
    _userRepository = userRepository;
    _mapper = mapper;
    _roleRepository = roleRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(
    RegisterCommand request,
    CancellationToken cancellationToken = default)
  {
    var role = await _roleRepository.GetByNameAsync(request.Role, cancellationToken)
               ?? throw new InvalidRoleException(UserMessages.InvalidRole);

    if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
    {
      throw new UserWithEmailAlreadyExistsException(UserMessages.WithEmailExists);
    }

    var userToAdd = _mapper.Map<User>(request);

    userToAdd.Roles.Add(role);

    await _userRepository.CreateAsync(userToAdd, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}