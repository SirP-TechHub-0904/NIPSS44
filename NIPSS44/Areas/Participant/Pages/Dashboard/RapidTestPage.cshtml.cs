using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Participant.Pages.Dashboard
{
[Authorize]
    public class RapidTestPageModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RapidTestPageModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<UserAnswer> UserAnswer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var Profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            UserAnswer = await _context.UserAnswers.Include(x => x.Profile).Where(c=>c.ProfileId == Profile.Id).OrderByDescending(x=>x.Date).ToListAsync();
            return Page();
        }
    }
}
