using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.Rapid
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class RapidComposeModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RapidComposeModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Profile Profile { get; set; }
        public async Task<IActionResult> OnGet()
        {

            return Page();
        }

        [BindProperty]
        public UserAnswer UserAnswer { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var xprofile = await _context.Profiles
                .Include(p => p.Alumni)
                .Include(p => p.User).Where(x => x.IsParticipant == true).ToListAsync();

            foreach (var x in xprofile)
            {
                UserAnswer ux = new UserAnswer();
                ux.Title = UserAnswer.Title;
                ux.Date = DateTime.UtcNow.AddHours(1);
                ux.StartTime = DateTime.UtcNow.AddHours(1);
                ux.ProfileId = x.Id;
                _context.UserAnswers.Add(UserAnswer);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./RapidList", new { id = UserAnswer.Title });
        }
    }

}
