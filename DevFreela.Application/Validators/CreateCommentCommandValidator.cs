using DevFreela.Application.Commands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(comment => comment.Content)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo do Conteudo é de 255 caracteres.");

            RuleFor(comment => comment.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Conteúdo do comentário é obrigatório!");
        }
    }
}
