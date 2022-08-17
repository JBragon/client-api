using FluentValidation;
using Models.Infrastructure;

namespace Models.Mapper.Request
{
    public class ClientPost
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string CPF { get; set; }
    }

    public class ClientPostValidation : AbstractValidator<ClientPost>
    {
        public ClientPostValidation()
        {
            RuleFor(v => v.Name)
              .NotEmpty()
              .WithMessage(RuleMessage.Informed("{PropertyName}"))
              .MaximumLength(255);

            RuleFor(v => v.State)
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
