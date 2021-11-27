using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Infrastructure.Validators
{
    public class CommentValidator : AbstractValidator<CommentDto>
    {
        public CommentValidator()
        {
            RuleFor(com => com.Description)
                .NotNull()
                .WithMessage("El Comentario no puede estár vacío")
                .Length(10, 1000)
                .WithMessage("La longitud de la Despcripción debe estar entre 10 y 1000 caracteres");
        }
    }
}
