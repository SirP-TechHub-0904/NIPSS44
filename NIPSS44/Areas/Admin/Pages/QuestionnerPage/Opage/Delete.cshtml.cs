using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.QuestionnerPage.Opage
{
    public class DeleteModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DeleteModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Option Option { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Option = await _context.Options
                .Include(o => o.Question).FirstOrDefaultAsync(m => m.Id == id);

            if (Option == null)
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

            Option = await _context.Options.FindAsync(id);

            if (Option != null)
            {
                _context.Options.Remove(Option);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
