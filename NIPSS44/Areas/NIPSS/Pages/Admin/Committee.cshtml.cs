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
    public class CommitteeModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public CommitteeModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<CommitteeCategory> CommitteeCategory { get;set; }

        public async Task OnGetAsync()
        {
            CommitteeCategory = await _context.CommitteeCategories.Include(x=>x.Alumni).Include(x=>x.Committees).Where(X=>X.Alumni.Active == true).ToListAsync();
        }
    }
}
