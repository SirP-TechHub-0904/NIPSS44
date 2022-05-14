using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.NIPSS.Pages.Menu
{
    public class RapidTestModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public RapidTestModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<RapidTest> RapidTest { get;set; }

        public async Task OnGetAsync()
        {
            RapidTest = await _context.RapidTest.ToListAsync();
        }
    }
}
