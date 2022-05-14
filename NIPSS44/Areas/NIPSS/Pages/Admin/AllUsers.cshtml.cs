using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.NIPSS.Pages.Admin
{
    public class AllUsersModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public AllUsersModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> Profile { get;set; }

        public async Task OnGetAsync()
        {
            Profile = await _context.Profiles
                .Include(p => p.Alumni)
                .Include(p => p.User).ToListAsync();
        }
    }
}
