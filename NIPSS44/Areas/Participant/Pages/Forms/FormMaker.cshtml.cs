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

namespace NIPSS44.Areas.Participant.Pages.Forms
{

    [Authorize]
    public class FormMakerModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FormMakerModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Questionner Questionner { get; set; }

        public async Task<IActionResult> OnGetAsync(string o = null, string q = null)
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (o != null)
            {
                Questionner = await _context.Questionners.FirstOrDefaultAsync(x => x.ShortLink == o);
            }
            else
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
