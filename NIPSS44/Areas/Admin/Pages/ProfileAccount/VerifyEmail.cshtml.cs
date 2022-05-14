using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NIPSS44.Data;

namespace NIPSS44.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin")]

    public class VerifyEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly NIPSSDbContext _context;

        public VerifyEmailModel(NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGetAsync(string uid)
        {
           

            try
            {
                var user = await _userManager.FindByIdAsync(uid);
               
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
            catch (Exception s)
            {

            }
           

            return RedirectToPage("./Details", new { uid = uid });
        }

    }
}
