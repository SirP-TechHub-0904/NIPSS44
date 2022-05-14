using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.ProfileAccount
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class IndexModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public IndexModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Profile> Profile { get;set; }
        public int Participant { get;set; }
        public int Staff { get;set; }

        public async Task OnGetAsync()
        {

            IQueryable<Profile> profilex = from s in _context.Profiles.Include(x=>x.User).Where(x => x.User.Email != "jinmcever@gmail.com").OrderByDescending(x => x.FullName)
                                     select s;
            Profile = await profilex.ToListAsync();
           //var xProfile = await profilex.Where(x=>x.AccountRole == "Participant").ToListAsync();
           // //var docv = await _context.Documents.Where(x => x.CoverImage == "~/91List-Doc.jpeg").ToListAsync();
           // foreach (var x in xProfile)
           // {
           //     x.Sent = false;

           //     _context.Attach(x).State = EntityState.Modified;

           // }
           // await _context.SaveChangesAsync();

            Participant = await profilex.Where(x => x.AccountRole == "Participant").CountAsync();
            Staff = await profilex.Where(x => x.AccountRole == "Staff").CountAsync();
        }
    }
}
