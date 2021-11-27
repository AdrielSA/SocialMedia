using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotNull()
                .WithMessage("Debe ingresar un Nombre");

            RuleFor(user => user.LastName)
                .NotNull()
                .WithMessage("Debe ingresar un Apellido");

            RuleFor(user => user.Email)
                .NotNull()
                .WithMessage("Debe ingresar un Correo")
                .EmailAddress()
                .WithMessage("Debe ingresar un Correo Válido");
        }
    }
}
