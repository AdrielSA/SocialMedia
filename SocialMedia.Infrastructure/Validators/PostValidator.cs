using FluentValidation;
using SocialMedia.Core.DTOs;
using System;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(Post => Post.Description)
                .Length(10, 1000)
                .WithMessage("La longitud de la Despcripción debe estar entre 10 y 1000 caracteres")
                .NotNull()
                .WithMessage("Debe ingresar una Descripción");

            RuleFor(Post => Post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
