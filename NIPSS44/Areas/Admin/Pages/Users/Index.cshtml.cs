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

namespace NIPSS44.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class IndexModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public IndexModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<IdentityUser> Profile { get; set; }

        public async Task OnGetAsync()
        {
            Profile = await _context.Users.Where(x => x.Email != "jinmcever@gmail.com").ToListAsync();
        }
    }
}