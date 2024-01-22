using Microsoft.AspNetCore.Identity;

namespace PracticeCA.Domain;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
}
