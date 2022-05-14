using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class UpdateRoleModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UpdateRoleModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Profile> Profile { get;set; }

        public async Task OnGetAsync()
        {
            Profile = await _context.Profiles
                .Include(p => p.User).Where(x=>x.User.Email != "jinmcever@gmail.com").ToListAsync();
            var userss= await _context.Users.Where(x => x.Email != "jinmcever@gmail.com").ToListAsync();
            foreach (var i in userss)
            {
                try
                {
                    await _userManager.AddToRoleAsync(i, "Participant");
                }catch(Exception d)
                {

                }
            }
        }
    }
}
