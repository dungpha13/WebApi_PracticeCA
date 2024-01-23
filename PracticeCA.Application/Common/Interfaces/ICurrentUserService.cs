namespace PracticeCA.Application;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
    Task<bool> IsInRoleAsync(string role);
    Task<bool> AuthorizeAsync(string policy);
}
