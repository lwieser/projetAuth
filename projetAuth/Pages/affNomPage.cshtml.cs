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
    public class affNomPage : PageModel
    {
        private UserManager<MyUser> _userManager;
        private ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        public MyUser CurrentUser { get; set; }
            
        public affNomPage(UserManager<MyUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }
       
        public void OnGet()
        { 
            SetUser();
            
        }

        public async Task<IActionResult> OnPost(MyUser formUser)
        {
            SetUser();
            var isAdmin = HttpContext.Request.Form["isAdmin"].FirstOrDefault() == "on";
            if (isAdmin)
            {
                var res = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                var res2 = await _userManager.AddToRoleAsync(CurrentUser, "Admin");

            }
            else
            {
                await _userManager.RemoveFromRoleAsync(CurrentUser, "Admin");
            }
            CurrentUser.LastName = formUser.LastName;
            _context.Users.Attach(CurrentUser).State = EntityState.Modified;
            _context.SaveChanges();

            return Page();
        }

        private void SetUser()
        {
            var userEmail = HttpContext.User.Identity.Name;
            var currentUser = _userManager.Users.FirstOrDefault(x => x.Email == userEmail);
            TempData["isAdmin"] = HttpContext.User.IsInRole("Admin") ? "checked" : "";
            CurrentUser = currentUser;
        }
    }
}