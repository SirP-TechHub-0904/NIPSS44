using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NIPSS44.Data;
using NIPSS44.Data.Model;

namespace NIPSS44.Pages.TourPage
{
    public class NipssTourModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public NipssTourModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public IList<TourCategory> TourCategory { get;set; }
        public IList<StudyGroup> StudyGroup { get;set; }

        public async Task OnGetAsync()
        {
            TourCategory = await _context.TourCategories.Include(x=>x.TourSubCategories).ToListAsync();
            StudyGroup = await _context.StudyGroups.ToListAsync();


        }
    }
}
