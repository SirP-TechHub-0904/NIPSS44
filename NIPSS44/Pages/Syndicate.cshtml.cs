using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages
{
    public class SyndicateModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public SyndicateModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<StudyGroup> StudyGroup { get;set; }

        public async Task OnGetAsync()
        {
            StudyGroup = await _context.StudyGroups.Include(x=>x.StudyGroupMemebers).OrderBy(x=>x.SortNumber).ToListAsync();
        }
    }
}
