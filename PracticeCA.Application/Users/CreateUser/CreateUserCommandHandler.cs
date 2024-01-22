using MediatR;
using Microsoft.AspNetCore.Identity;
using PracticeCA.Domain;
using PracticeCA.Domain.Common.Exceptions;

namespace PracticeCA.Application;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;

    public CreateUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

        var existingUser = await _userManager.FindByNameAsync(request.Email);

        if (existingUser is not null)
        {
            throw new DuplicateException("This Email is already taken!");
        }

        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName
        };

        var roleExists = await _roleManager.RoleExistsAsync("Employee");

        if (!roleExists)
        {
            var roleResult = await _roleManager.CreateAsync(new Role() { Name = "Employee" });

            if (!roleResult.Succeeded)
            {
                throw new Exception($"{roleResult.Errors}");
            }
        }

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new Exception($"{result.Errors}");
        }

        await _userManager.AddToRoleAsync(user, "Employee");

        return user.Id;
    }

}
