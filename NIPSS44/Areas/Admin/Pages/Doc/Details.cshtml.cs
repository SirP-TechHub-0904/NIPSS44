using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.Doc
{
    public class DetailsModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public DetailsModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public DocumentCategory DocumentCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DocumentCategory = await _context.DocumentCategories
                .Include(d => d.Alumni).FirstOrDefaultAsync(m => m.Id == id);

            if (DocumentCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
