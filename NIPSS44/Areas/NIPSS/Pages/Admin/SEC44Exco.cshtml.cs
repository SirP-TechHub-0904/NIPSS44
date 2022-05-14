using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Areas.NIPSS.Pages.Admin
{
    public class SEC44ExcoModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public SEC44ExcoModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Executive> Executive { get;set; }

        public async Task OnGetAsync()
        {
            Executive = await _context.Executive.Include(x => x.Profile).ThenInclude(x => x.StudyGroupMemeber).ThenInclude(x=>x.StudyGroup).Include(x => x.Alumni).OrderBy(x => x.SortOrder).Where(x => x.Alumni.Active == true).ToListAsync();
        }
    }
}
