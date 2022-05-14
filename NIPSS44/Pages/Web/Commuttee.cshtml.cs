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
    public class CommutteeModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public CommutteeModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<CommitteeCategory> CommitteeCategory { get;set; }

        public async Task OnGetAsync()
        {
            CommitteeCategory = await _context.CommitteeCategories
                .Include(c => c.Alumni).ToListAsync();
        }
    }
}
