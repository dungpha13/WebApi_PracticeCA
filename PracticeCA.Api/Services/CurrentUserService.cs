using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using PracticeCA.Application;

namespace PracticeCA.Api;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal? _claimsPrincipal;
    private readonly IAuthorizationService _authorizationService;
    private readonly HttpContext _context;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService)
    {
        _claimsPrincipal = httpContextAccessor?.HttpContext?.User;
        _authorizationService = authorizationService;
        _context = httpContextAccessor?.HttpContext;
        MapJwtClaimsToIdentityClaims();
    }

    public string? UserId => _claimsPrincipal?.FindFirst("nameid")?.Value;
    public string? UserName => _claimsPrincipal?.FindFirst("username")?.Value;
    public string? Role => _claimsPrincipal?.FindFirst("role")?.Value;
    public string? accessToken => _context.Request.Headers["Authorization"];

    private void MapJwtClaimsToIdentityClaims()
    {
        if (_claimsPrincipal == null)
            return;

        var identity = _claimsPrincipal.Identity as ClaimsIdentity;
        if (identity == null)
            return;

        string authorizationHeader = _context.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            string token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                foreach (var claim in jsonToken.Claims)
                {
                    if (!identity.HasClaim(c => c.Type == claim.Type))
                    {
                        identity.AddClaim(new Claim(claim.Type, claim.Value));
                    }
                }
            }
        }
    }

    public async Task<bool> AuthorizeAsync(string policy)
    {
        if (_claimsPrincipal == null) return false;
        return (await _authorizationService.AuthorizeAsync(_claimsPrincipal, policy)).Succeeded;
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        return await Task.FromResult(_claimsPrincipal?.IsInRole(role) ?? false);
    }
}
