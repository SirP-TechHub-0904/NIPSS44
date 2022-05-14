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
    public class DirectingStaffModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DirectingStaffModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> DS { get;set; }

        public async Task OnGetAsync()
        {
            DS = await _context.Profiles.Where(x => x.AccountRole == "DirectingStaff").ToListAsync();
        }
    }
}
