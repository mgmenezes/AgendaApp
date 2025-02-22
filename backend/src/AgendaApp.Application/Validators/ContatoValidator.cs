// AgendaApp.Application/Validators/ContatoValidator.cs
using FluentValidation;
using AgendaApp.Application.DTOs;

namespace AgendaApp.Application.Validators
{

    public class CriarContatoValidator : AbstractValidator<CriarContatoDto>
    {
        public CriarContatoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório")
                .EmailAddress().WithMessage("Email inválido")
                .MaximumLength(100).WithMessage("O email deve ter no máximo 100 caracteres");

            // RuleFor(x => x.Telefone)
            //     .NotEmpty().WithMessage("O telefone é obrigatório")
            //     .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
            //     .WithMessage("O telefone deve estar no formato (99) 99999-9999");
        }
    }

    // public class AtualizarContatoValidator : AbstractValidator<AtualizarContatoDto>
    // {
    //     public AtualizarContatoValidator()
    //     {
    //         RuleFor(x => x.Id)
    //             .NotEmpty().WithMessage("O Id é obrigatório");

    //         RuleFor(x => x.Nome)
    //             .NotEmpty().WithMessage("O nome é obrigatório")
    //             .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres");

    //         RuleFor(x => x.Email)
    //             .NotEmpty().WithMessage("O email é obrigatório")
    //             .EmailAddress().WithMessage("Email inválido")
    //             .MaximumLength(100).WithMessage("O email deve ter no máximo 100 caracteres");

    //         RuleFor(x => x.Telefone)
    //             .NotEmpty().WithMessage("O telefone é obrigatório")
    //             .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
    //             .WithMessage("O telefone deve estar no formato (99) 99999-9999");
    //     }
    // }
}