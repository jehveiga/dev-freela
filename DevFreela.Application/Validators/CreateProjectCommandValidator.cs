using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(project => project.Description)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo de Descrição é de 255 caracteres.");

            RuleFor(project => project.Description)
                .NotEmpty()
                .WithMessage("Descrição não poder ser vazio!")
                .NotNull()
                .WithMessage("Descrição é obrigatória!");

            RuleFor(project => project.Title)
                .MaximumLength(30)
                .WithMessage("Tamanho máximo de Título é de 30 caracteres");

            RuleFor(project => project.Title)
                .NotEmpty()
                .WithMessage("Título não poder ser vazio!")
                .NotNull()
                .WithMessage("Título é obrigatório!");
        }
    }
}
