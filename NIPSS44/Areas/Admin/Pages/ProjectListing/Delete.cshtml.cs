using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.ProjectListing
{
    public class DeleteModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DeleteModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LegacyProject LegacyProject { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LegacyProject = await _context.LegacyProjects.FirstOrDefaultAsync(m => m.Id == id);

            if (LegacyProject == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LegacyProject = await _context.LegacyProjects.FindAsync(id);

            if (LegacyProject != null)
            {
                _context.LegacyProjects.Remove(LegacyProject);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
