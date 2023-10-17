using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using Microsoft.AspNetCore.Identity;
using projetAuth.Data;

namespace projetAuth.Pages
{
    [Authorize(Roles = "Admin")]
    public class affNomPage : PageModel
    {
        private UserManager<MyUser> _userManager;
        private ApplicationDbContext _context;
        public MyUser CurrentUser { get; set; }
            
        public affNomPage(UserManager<MyUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
       
        public void OnGet()
        { 
            SetUser();
        }

        public IActionResult OnPost(MyUser formUser)
        {
            SetUser();
            CurrentUser.LastName = formUser.LastName;
            _context.Users.Attach(CurrentUser).State = EntityState.Modified;
            _context.SaveChanges();

            return Page();
        }

        private void SetUser()
        {
            var userEmail = HttpContext.User.Identity.Name;
            var currentUser = _userManager.Users.FirstOrDefault(x => x.Email == userEmail);
            CurrentUser = currentUser;
        }
    }
}