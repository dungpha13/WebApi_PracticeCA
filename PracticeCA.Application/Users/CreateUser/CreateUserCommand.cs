using MediatR;

namespace PracticeCA.Application;

public class CreateUserCommand : IRequest<Guid>, ICommand
{
    public CreateUserCommand(string firstName, string email, string password, string confirmPassword)
    {
        FirstName = firstName;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
