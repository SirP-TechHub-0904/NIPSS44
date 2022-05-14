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
    public class CommitteeMembersModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public CommitteeMembersModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<Committee> Committee { get;set; }
        public CommitteeCategory CommitteeCategory { get; set; }

        public async Task OnGetAsync(long id)
        {
            Committee = await _context.Committees
                .Include(c => c.CommitteeCategory)
                .Include(c => c.Profile).ThenInclude(c => c.StudyGroupMemeber).ThenInclude(c => c.StudyGroup).Where(x=>x.CommitteeCategoryId == id).ToListAsync();

            CommitteeCategory = await _context.CommitteeCategories.Include(x => x.Alumni).Include(x => x.Committees).Where(X => X.Alumni.Active == true).FirstOrDefaultAsync(x=>x.Id == id);

        }
    }
}
