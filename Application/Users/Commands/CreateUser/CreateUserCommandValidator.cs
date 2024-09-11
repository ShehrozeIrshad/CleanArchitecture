using FluentValidation;

namespace Application.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandValidator :  AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
                RuleFor(x => x.email).NotEmpty();
                RuleFor(x => x.firstName).NotEmpty();
                RuleFor(x => x.lastName).NotEmpty();
                RuleFor(x => x.password).NotEmpty();
        }
    }
}
