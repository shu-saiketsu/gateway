using FluentValidation;
using Saiketsu.Gateway.Domain.Enums;

namespace Saiketsu.Gateway.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100)
            .Matches("[a-z]+")
            .Matches("[A-Z]+")
            .Matches("[0-9]+");

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.Role).Custom((@enum, context) =>
        {
            if (@enum == null) return;

            var isValid = Enum.IsDefined(typeof(RoleEnum), @enum);
            if (!isValid) context.AddFailure("Enum is not valid.");
        });
    }
}