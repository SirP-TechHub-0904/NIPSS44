using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages.Web
{
    public class CommutteeListModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public CommutteeListModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Committee> Committee { get;set; }
        public CommitteeCategory CommitteeCategory { get; set; }

        public async Task OnGetAsync(long id)
        {
            Committee = await _context.Committees
                .Include(c => c.Profile).Where(x=>x.CommitteeCategoryId == id).OrderBy(s=>s.SortOrder).ToListAsync();

            CommitteeCategory = await _context.CommitteeCategories.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
