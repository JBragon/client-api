using FluentValidation;
using Models.Infrastructure;

namespace Models.Mapper.Request
{
    public class ClientPost
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
        public string CPF { get; set; }
    }

    public class ClientPostValidation : AbstractValidator<ClientPost>
    {
        public ClientPostValidation()
        {
            RuleFor(v => v.Nome)
              .NotEmpty()
              .WithMessage(RuleMessage.Informed("{PropertyName}"))
              .MaximumLength(255);

            RuleFor(v => v.Estado)
             .NotEmpty()
             .WithMessage(RuleMessage.Informed("{PropertyName}"))
             .MaximumLength(2);

            RuleFor(v => v.CPF)
             .NotEmpty()
             .WithMessage(RuleMessage.Informed("{PropertyName}"))
             .IsValidCPF()
             .WithMessage(Resources.Common.InvalidCPF)
             .MaximumLength(14);
        }
    }
}
