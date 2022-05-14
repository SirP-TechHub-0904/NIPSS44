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
    public class AlumniSectionModel : PageModel
    {
        private readonly NIPSS44.Data.NIPSSDbContext _context;

        public AlumniSectionModel(NIPSS44.Data.NIPSSDbContext context)
        {
            _context = context;
        }

        public Alumni Alumni { get; set; }

        public int Male {get;set;}
        public int Female { get;set;}

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Alumni = await _context.Alumnis
                .Include(x=>x.SecProject)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Alumni == null)
            {
                return NotFound();
            }

            IQueryable<Profile> profilex = from s in _context.Profiles
                                                .Where(x => x.AlumniId == id)
                                            select s;

            Male = profilex.Where(x => x.Gender.ToUpper() == "MALE").Count();
            Female = profilex.Where(x => x.Gender.ToUpper() == "FEMALE").Count();
            return Page();
        }
    }
}
