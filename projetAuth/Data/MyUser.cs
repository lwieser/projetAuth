using Microsoft.AspNetCore.Identity;

namespace projetAuth.Data;

public class MyUser : IdentityUser
{
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
}