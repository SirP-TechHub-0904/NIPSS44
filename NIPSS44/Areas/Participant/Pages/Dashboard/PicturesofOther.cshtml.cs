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

namespace NIPSS44.Areas.Participant.Pages.Dashboard
{
    [Authorize]
    public class PicturesofOtherModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public PicturesofOtherModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Gallery> Gallery { get;set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            Gallery = await _context.Galleries
                .Include(g => g.Profile).Where(x=>x.ProfileId != profile.Id && x.Private == false && x.DontShow == false).OrderByDescending(x=>x.Date).ToListAsync();
        }
    }
}
