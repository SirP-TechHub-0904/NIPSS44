using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages
{
    public class TermModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public TermModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }
       
        public IActionResult OnGet()
        {

            return Page();
        }
 }
}
