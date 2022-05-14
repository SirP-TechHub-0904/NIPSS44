using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.GroupMemberPage
{
    public class CreateModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public CreateModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "FullName");
        ViewData["StudyGroupId"] = new SelectList(_context.StudyGroups, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public StudyGroupMemeber StudyGroupMemeber { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StudyGroupMemebers.Add(StudyGroupMemeber);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
