using FluentValidation;

namespace Application.Users.Commands.Login
{
    public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.email).NotEmpty();
        }
    }
}
