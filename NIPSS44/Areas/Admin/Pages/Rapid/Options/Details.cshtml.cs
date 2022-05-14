using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.Rapid.Options
{
    public class DetailsModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DetailsModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public RapidOption RapidOption { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RapidOption = await _context.RapidOption
                .Include(r => r.RapidQuestion).FirstOrDefaultAsync(m => m.Id == id);

            if (RapidOption == null)
            {
                return NotFound();
            }
            return Page();
        }

    }
}
