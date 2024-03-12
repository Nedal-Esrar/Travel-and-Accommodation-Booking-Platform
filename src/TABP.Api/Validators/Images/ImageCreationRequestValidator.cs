using FluentValidation;
using TABP.Api.Dtos.Images;
using TABP.Application.Extensions.Validation;

namespace TABP.Api.Validators.Images;

public class ImageCreationRequestValidator : AbstractValidator<ImageCreationRequest>
{
  public ImageCreationRequestValidator()
  {
    RuleFor(x => x.Image)
      .NotEmpty()
      .ValidImage();
  }
}