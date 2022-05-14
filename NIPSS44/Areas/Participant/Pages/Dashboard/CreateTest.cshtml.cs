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

namespace NIPSS44.Areas.Participant.Pages.Dashboard
{
    [Authorize]
    public class CreateTestModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateTestModel(NIPSS44.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Profile Profile { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            Profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == user.Id);

            return Page();
        }

        [BindProperty]
        public UserAnswer UserAnswer { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UserAnswer.Date = DateTime.UtcNow.AddHours(1);
            UserAnswer.StartTime = DateTime.UtcNow.AddHours(1);
            _context.UserAnswers.Add(UserAnswer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./StartPreview", new { id = UserAnswer.Id });
        }
    }
}
