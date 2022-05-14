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
    public class StudyGroupsModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public StudyGroupsModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<StudyGroup> StudyGroup { get;set; }

        public async Task OnGetAsync()
        {
            StudyGroup = await _context.StudyGroups.Include(x=>x.StudyGroupMemebers).ThenInclude(x=>x.Profile).ThenInclude(c=>c.User).ToListAsync();
        }
    }
}
