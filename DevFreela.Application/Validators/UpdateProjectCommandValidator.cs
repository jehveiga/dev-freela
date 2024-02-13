using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(project => project.Description)
                .MaximumLength(200)
                .WithMessage("Tamanho máximo de Descrição é de 200 caracteres.");

            RuleFor(project => project.Description)
                .NotNull()
                .WithMessage("Descrição não poder ser vazio!")
                .NotEmpty()
                .WithMessage("Descrição é obrigatória!");

            RuleFor(project => project.Title)
                .MaximumLength(30)
                .WithMessage("Tamanho máximo de Título é de 30 caracteres");

            RuleFor(project => project.Title)
                .NotNull()
                .WithMessage("Título é obrigatório!")
                .NotEmpty()
                .WithMessage("Título não poder ser vazio!");
        }
    }
}
