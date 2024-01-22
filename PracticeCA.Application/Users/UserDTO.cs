using AutoMapper;
using PracticeCA.Domain;

namespace PracticeCA.Application;

public class UserDto : IMapFrom<User>
{
    public UserDto()
    {
    }

    public static UserDto Create(string userName, string firstName)
    {
        return new UserDto
        {
            Username = userName,
            FirstName = firstName,
        };
    }

    public string Username { get; set; }
    public string FirstName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>();
    }

}
