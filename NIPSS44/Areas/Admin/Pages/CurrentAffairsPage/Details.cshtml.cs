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

namespace NIPSS44.Areas.Admin.Pages.CurrentAffairsPage
{
    [Authorize(Roles = "mSuperAdmin,Admin,Content")]

    public class DetailsModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DetailsModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public CurrentAffair CurrentAffair { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurrentAffair = await _context.CurrentAffairs.FirstOrDefaultAsync(m => m.Id == id);

            if (CurrentAffair == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
