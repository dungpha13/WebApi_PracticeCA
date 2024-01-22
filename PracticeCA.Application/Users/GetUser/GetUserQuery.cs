using MediatR;

namespace PracticeCA.Application;

public class GetUserQuery : IRequest<UserDto>, IQuery
{
    public GetUserQuery(string userName, string password)
    {
        Username = userName;
        Password = password;
    }

    public string Username { get; set; }
    public string Password { get; set; }
}
