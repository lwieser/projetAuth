using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace projetAuth.Pages.Admin;

[Authorize(Roles = "Admin")]
public class UserList : PageModel
{
    public void OnGet()
    {
        
    }
}