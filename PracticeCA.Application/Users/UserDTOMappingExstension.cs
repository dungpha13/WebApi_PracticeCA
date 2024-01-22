using AutoMapper;
using PracticeCA.Domain;

namespace PracticeCA.Application;

public static class UserDtoMappingExstension
{
    public static UserDto MapToUserDto(this User user, IMapper mapper)
    => mapper.Map<UserDto>(user);
}
