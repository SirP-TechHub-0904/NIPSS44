using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.Admin.Pages.AlumniPage.ExecutivePage
{
    public class ExecutiveListModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public ExecutiveListModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Executive> Executive { get;set; }
        public Alumni Alumni { get; set; }
        public async Task OnGetAsync(long id)
        {
            Executive = await _context.Executive
                .Include(e => e.Alumni)
                .Include(e => e.Profile)
                .Where(x=>x.AlumniId == id)
                .ToListAsync();

            Alumni = await _context.Alumnis.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
