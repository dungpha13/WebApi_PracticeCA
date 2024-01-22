using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeCA.Domain;

namespace PracticeCA.Application;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user is null)
        {
            throw new NotFoundException($"Could not find UserName '{request.Username}'");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new NotFoundException("Wrong credentials");
        }

        return user.MapToUserDto(_mapper);
    }
}
