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
                .NotEmpty()
                .WithMessage("Descrição é obrigatória!");

            RuleFor(project => project.Title)
                .MaximumLength(30)
                .WithMessage("Tamanho máximo de Título é de 30 caracteres");

            RuleFor(project => project.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Título é obrigatório!");
        }
    }
}
