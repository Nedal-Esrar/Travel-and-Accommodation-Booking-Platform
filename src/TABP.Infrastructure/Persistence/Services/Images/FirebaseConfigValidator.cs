using FluentValidation;
using TABP.Infrastructure.Common;

namespace TABP.Infrastructure.Persistence.Services.Images;

public class FireBaseConfigValidator : AbstractValidator<FirebaseConfig>
{
  public FireBaseConfigValidator()
  {
    RuleFor(x => x.Bucket)
      .NotEmpty();

    RuleFor(x => x.CredentialsJson)
      .NotEmpty()
      .ValidJson();
  }
}